using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventLibrary>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();

            if (events == null) return NotFound("There are no events");
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventLibrary>> GetEventById(int id)
        {
            var seminar = await _eventService.GetEventById(id);

            if (seminar == null)
            {
                return NotFound("There is no event with that ID");
            }
            return Ok(seminar);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvent([FromBody] EventLibrary eventToAdd)
        {
            if (eventToAdd != null)
            {
                await _eventService.CreateEvent(eventToAdd);

                return Ok("Seminar has been added");
            }
            return BadRequest("Could not add");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(int id, [FromBody] EventLibrary seminarToUpdate)
        {
            var seminar = await _eventService.UpdateEvent(id, seminarToUpdate);

            if (seminar != null) return Ok("Seminar has been updated");
            return NotFound("There is no event with that ID");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var eventt = await _eventService.DeleteEvent(id);
            if (eventt != null) return Ok("Event has been deleted");
            return NotFound("There is no event with that ID");
        }
    }
}
