using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorService authorService, ApplicationDbContext context, IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();

            if (authors == null || !authors.Any())
            {
                return NotFound("Authors were not found");
            }

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return NotFound("Author was not found");
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author addAuthor)
        {
            var authorExists = _context.Authors
                .FirstOrDefault(p => p.Name.ToLower() == addAuthor.Name.ToLower());

            if (authorExists == null)
            {
                await _authorRepository.AddAsync(addAuthor);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Author has been created");
            }
            return BadRequest("Author already exists");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Author updatedAuthor)
        {
            var authorToUpdate = await _authorRepository.GetByIdAsync(id);
            if (authorToUpdate == null)
                return NotFound("Author was not found");

            authorToUpdate.Name = updatedAuthor.Name;

            _context.Update(authorToUpdate);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Author has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var authorToDelete = await _authorRepository.GetByIdAsync(id);
            if (authorToDelete == null)
                return NotFound("Author was not found");

            _context.Authors.Remove(authorToDelete);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Author has been deleted");
        }
    }
}
