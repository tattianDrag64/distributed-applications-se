using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotekBoklusen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categoryList = _context.Categories.ToList();

            if (categoryList == null)
            {
                return NotFound("Category was not found");
            }

            return Ok(categoryList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound("Category was not found");
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category categoryToAdd)
        {
            var productExists = _context.Categories
                .FirstOrDefault(p => p.CategoryName.ToLower() == categoryToAdd.CategoryName.ToLower());

            if (productExists == null)
            {
                _context.Categories.Add(categoryToAdd);
                await _context.SaveChangesAsync();
                return Ok("Category has been created");
            }
            return BadRequest("Something went wrong");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Category newCategoryValues)
        {
            var categoryToChange = await _context.Categories.FindAsync(id);
            if (categoryToChange == null)
                return NotFound("Category was not found");

            categoryToChange.CategoryName = newCategoryValues.CategoryName;

            _context.Update(categoryToChange);
            await _context.SaveChangesAsync();
            return Ok("Category has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);
            if (categoryToDelete == null)
                return NotFound("Category was not found");

            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            return Ok("Category has been deleted");
        }
    }
}
