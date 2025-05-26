using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Implementations;
using ServerLibrary.Services.Interfaces;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound("Author was not found");
            return Ok(author);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAuthorDTO addAuthor)
        {
            // Map CreateAuthorDTO to AuthorDTO  
            var authorDto = new AuthorDTO
            {
                Name = addAuthor.Name,
                Biography = addAuthor.Biography,
                DateOfBirth = addAuthor.DateOfBirth,
                ImageUrl = addAuthor.ImageUrl,
                IsAlive = addAuthor.IsAlive,
                DateOfDeath = addAuthor.DateOfDeath
            };

            var result = await _authorService.CreateAsync(authorDto);
            if (result != null) // Assuming result is a boolean indicating success  
                return Ok("Author has been created");
            return BadRequest("Failed to create author"); // Adjusted error message  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateAuthorDTO updatedAuthor)
        {
            var authorDto = new AuthorDTO
            {
                Id = id, // Ensure the ID is set correctly
                Name = updatedAuthor.Name,
                Biography = updatedAuthor.Biography,
                DateOfBirth = updatedAuthor.DateOfBirth,
                ImageUrl = updatedAuthor.ImageUrl,
                IsAlive = updatedAuthor.IsAlive,
                DateOfDeath = updatedAuthor.DateOfDeath
            };
            var result = await _authorService.UpdateAsync(id, authorDto);
            if (result !=null) // Assuming result is a boolean indicating success  
                return Ok("Author has been updated");
            return NotFound("Failed to update author"); // Adjusted error message  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _authorService.DeleteAsync(id);
            if (result) // Assuming result is a boolean indicating success  
                return Ok("Author has been deleted");
            return NotFound("Failed to delete author"); // Adjusted error message  
        }
    }
}
