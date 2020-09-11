using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncInn.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Amenitites",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "WiFi" },
                    { 2, "Air Conditioning" },
                    { 3, "Coffee Maker" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "City", "Country", "Name", "Phone", "State" },
                values: new object[,]
                {
                    { 1L, "123 Sync Street", "Des Moines", null, "Async Inn", -1274, "Iowa" },
                    { 2L, "1858 Cyclone Lane", "Ames", null, "Async Motel", -890, "Iowa" },
                    { 3L, "5050 Morning Star Court", "Pleasant Hill", null, "Async Suites", -1018, "Iowa" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Studio", 2 },
                    { 2, "OneBedroom", 1 },
                    { 3, "TwoBedroom", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Amenitites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Amenitites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Amenitites",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
