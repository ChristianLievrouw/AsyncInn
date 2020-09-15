using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AsyncInn.Services;
using AsyncInn.Models;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoom repository;

        public HotelRoomController(IHotelRoom repository)
        {
            this.repository = repository;
        }
        // GET: api/HotelRoom
        [HttpGet]
        public async Task<IEnumerable<HotelRoom>> GetHotelRooms()
        {
            return await repository.GetAllHotelRooms();
        }

        // GET: api/HotelRoom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int id)
        {
            var hotelRoom = await repository.GetOneHotelRoom(id);

            if(hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // POST: api/HotelRoom
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/HotelRoom/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom(int id, HotelRoom hotelRoom)
        {
            if(id != hotelRoom.RoomId)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateHotelRoomAsync(hotelRoom);

            if(!didUpdate)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int id)
        {
            HotelRoom hotelRoom = await repository.DeleteOneHotelRoomById(id);

            if(hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }
    }
}
