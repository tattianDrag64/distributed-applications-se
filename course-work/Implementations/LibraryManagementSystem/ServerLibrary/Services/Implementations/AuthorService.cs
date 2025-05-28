using AutoMapper;
using Azure;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using SendGrid.Helpers.Errors.Model;
using Server.Services.Implementations;
using Server.Services.Interfaces;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Implementations
{
    public class AuthorService : ServicesBase<Author, AuthorDTO>, IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) // Added the missing parameter  
            : base(authorRepository, mapper, unitOfWork) // Pass the unitOfWork to the base class  
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Response<Author>>> GetAuthorByBookId(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId) ?? throw new NotFoundException("book not found");
            var author = await _authorRepository.GetAuthorByBookId(bookId);
            return author == null
                ? throw new NotFoundException("author not found")
                : new List<Response<Author>> { Response.FromValue(author, null) };
        }
    }
}
