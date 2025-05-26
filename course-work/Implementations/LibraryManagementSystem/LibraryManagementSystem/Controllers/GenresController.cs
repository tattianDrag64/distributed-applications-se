using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Implementations;
using ServerLibrary.Repositories.Interfaces;
using ServerLibrary.Services.Interfaces;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IGenreService _genreService;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GenresController(IGenreRepository genreRepository, IGenreService genreService, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _genreService = genreService;
            _unitOfWork = unitOfWork;   
        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            var categoryList = await _genreService.GetAllAsync();

            if (categoryList == null)
            {
                return NotFound("Category was not found");
            }

            return Ok(categoryList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var category = await _genreService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Category was not found");
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Genre categoryToAdd)
        {
            var genreExists = await _genreRepository.GetAllAsync();
            if (genreExists.Any(p => p.Name.ToLower() == categoryToAdd.Name.ToLower()))
            {
                return BadRequest("Genre with the same name already exists");
            }

            await _genreRepository.AddAsync(categoryToAdd);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Genre has been created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Genre newCategoryValues)
        {
            var genreToChange = await _genreRepository.GetByIdAsync(id);
            if (genreToChange == null)
                return NotFound("Genre was not found");

            genreToChange.Name = newCategoryValues.Name;
            genreToChange.Description = newCategoryValues.Description;

            _genreRepository.Update(genreToChange);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Genre has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genreToDelete = await _genreRepository.GetByIdAsync(id);
            if (genreToDelete == null)
                return NotFound("Genre was not found");

            _genreRepository.Remove(genreToDelete);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Category has been deleted");
        }
    }
}
