using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotekBoklusen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CreatorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Creator>>> Get()
        {
            var creators = _context.Creators.ToList();

            if (creators == null)
            {
                return NotFound("Author was not found");
            }

            return Ok(creators);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var creator = await _context.Creators.FindAsync(id);
            if (creator == null)
                return NotFound("Author was not found");
            return Ok(creator);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Creator creatorToAdd)
        {
            var creatorExists = _context.Creators
                .FirstOrDefault(p => p.FirstName.ToLower() == creatorToAdd.FirstName.ToLower() && p.LastName.ToLower() == creatorToAdd.LastName.ToLower());

            if (creatorExists == null)
            {
                _context.Creators.Add(creatorToAdd);
                await _context.SaveChangesAsync();
                return Ok("Author has been created");
            }
            return BadRequest("Something went wrong");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Creator creatorToAdd)
        {
            var creatorToUpdate = await _context.Creators.FindAsync(id);
            if (creatorToUpdate == null)
                return NotFound("Author was not found");

            creatorToUpdate.FirstName = creatorToAdd.FirstName;
            creatorToUpdate.LastName = creatorToAdd.LastName;

            _context.Update(creatorToUpdate);
            await _context.SaveChangesAsync();
            return Ok("Author has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var creatorToDelete = await _context.Creators.FindAsync(id);
            if (creatorToDelete == null)
                return NotFound("Author was not found");

            _context.Creators.Remove(creatorToDelete);
            await _context.SaveChangesAsync();
            return Ok("Author has been deleted");
        }
    }
}
