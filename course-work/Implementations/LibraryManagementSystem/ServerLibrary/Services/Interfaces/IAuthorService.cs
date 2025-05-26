using Azure;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Interfaces
{
    public interface IAuthorService : IServiceBase<AuthorDTO>
    {
        Task<List<Response<Author>>> GetAuthorByBookId(int bookId);
    }
}
