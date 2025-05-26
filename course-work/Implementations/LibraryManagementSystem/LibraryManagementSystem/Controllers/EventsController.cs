using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Services.Interfaces;

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
        public async Task<ActionResult<List<EventDTO>>> GetAllEvents()
        {
            var events = await _eventService.GetAllAsync();

            if (events == null) return NotFound("There are no events");
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetEventById(int id)
        {
            var seminar = await _eventService.GetByIdAsync(id);

            if (seminar == null)
            {
                return NotFound("There is no event with that ID");
            }
            return Ok(seminar);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvent([FromBody] EventDTO eventToAdd)
        {
            if (eventToAdd != null)
            {
                await _eventService.CreateAsync(eventToAdd);

                return Ok("Seminar has been added");
            }
            return BadRequest("Could not add");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(int id, [FromBody] EventDTO seminarToUpdate)
        {
            var seminar = await _eventService.UpdateAsync(id, seminarToUpdate);

            if (seminar != null) return Ok("Seminar has been updated");
            return NotFound("There is no event with that ID");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var eventt = await _eventService.DeleteAsync(id);
            if (eventt != null) return Ok("Event has been deleted");
            return NotFound("There is no event with that ID");
        }
    }
}
