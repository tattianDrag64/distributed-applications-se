using BibliotekBoklusen.Server.Services.SeminarService;
using Microsoft.AspNetCore.Mvc;

namespace BibliotekBoklusen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeminarsController : ControllerBase
    {
        private readonly ISeminarService _seminarService;

        public SeminarsController(ISeminarService seminarService)
        {
            _seminarService = seminarService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Seminarium>>> GetAllSeminars()
        {
            var seminar = await _seminarService.GetAllSeminars();

            if (seminar == null)
            {
                return NotFound("There are no seminars");
            }
            return Ok(seminar);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seminarium>> GetSeminarById(int id)
        {
            var seminar = await _seminarService.GetSeminarById(id);

            if (seminar == null)
            {
                return NotFound("There is no seminar with that ID");
            }
            return Ok(seminar);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSeminar([FromBody] Seminarium seminarToAdd)
        {
            if(seminarToAdd != null)
            {
                await _seminarService.CreateSeminar(seminarToAdd);

                return Ok("Seminar has been added");
            }
            return BadRequest("Could not add");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSeminar(int id, [FromBody] Seminarium seminarToUpdate)
        {
            var seminar =await _seminarService.UpdateSeminar(id, seminarToUpdate);

            if(seminar != null)
            {
                return Ok("Seminar has been updated");
            }
            return NotFound("There is no seminar with that ID");            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSeminar(int id)
        {
            var seminar =await _seminarService.DeleteSeminar(id);

            if(seminar != null)
            {
                return Ok("Seminar has been deleted");
            }
            return NotFound("There is no seminar with that ID");
        }
    }
}
