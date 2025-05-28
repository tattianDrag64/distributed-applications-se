using Azure;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using ServerLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    public interface IAuthorService : IServiceBase<AuthorDTO>
    {
        Task<List<Response<Author>>> GetAuthorByBookId(int bookId);
    }
}
