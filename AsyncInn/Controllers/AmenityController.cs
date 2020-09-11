using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Services;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityRepo repository;
        private readonly HotelDbContext _context;

        public AmenityController(IAmenityRepo repository, HotelDbContext context)
        {
            this.repository = repository;
            _context = context;
        }

        // GET: api/Amenity
        [HttpGet]
        public async Task<IEnumerable<Amenity>> GetAmenitites()
        {
            return await _context.Amenitites.ToListAsync();
        }

        // GET: api/Amenity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenity>> GetAmenity(int id)
        {
            var amenity = await _context.Amenitites.FindAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return amenity;
        }

        // PUT: api/Amenity/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }

            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Amenity
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
            _context.Amenitites.Add(amenity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amenity>> DeleteAmenity(int id)
        {
            var amenity = await _context.Amenitites.FindAsync(id);
            if (amenity == null)
            {
                return NotFound();
            }

            _context.Amenitites.Remove(amenity);
            await _context.SaveChangesAsync();

            return amenity;
        }

        private bool AmenityExists(int id)
        {
            return _context.Amenitites.Any(e => e.Id == id);
        }
    }
}
