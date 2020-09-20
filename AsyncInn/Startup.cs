using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Services;
using AsyncInn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace AsyncInn
{
    public class Startup
    {
        //1
        public IConfiguration Configuration { get; }
        // 2
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });

            services.AddControllers()
                // No infinite reference looping here.
                .AddNewtonsoftJson(OptionsBuilderConfigurationExtensions =>
                {
                    OptionsBuilderConfigurationExtensions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddMvc();
            services.AddControllers();
            // step 3 Register our DbContext
            services.AddDbContext<HotelDbContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<HotelDbContext>();

            services.AddTransient<IUserService, IdentityUserService>();
            services.AddScoped<JwtTokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = JwtTokenService.GetValidationParameters(Configuration);
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("create", policy => policy.RequireClaim("permission", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permission", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permission", "delete"));
            });

            services.AddTransient<IHotelRepo, DatabaseHotelRepo>();
            services.AddTransient<IRoomRepo, DatabaseRoomRepo>();
            services.AddTransient<IAmenityRepo, DatabaseAmenityRepo>();
            services.AddTransient<IHotelRoom, DatabaseHotelRoomRepo>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Info", Version = "v1"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.OperationFilter<AuthenticationRequrementOperationFilter>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Hotel Info");
                options.RoutePrefix = "";
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        private class AuthenticationRequrementOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var hasAnonymous = context.ApiDescription.CustomAttributes().OfType<AllowAnonymousAttribute>().Any();
                if (hasAnonymous)
                    return;

                operation.Security ??= new List<OpenApiSecurityRequirement>();

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,
                    },
                };

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [scheme] = new List<string>()
                });
            }
        }
    }
}
