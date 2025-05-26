using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author?> GetAuthorWithBooksAsync(int authorId);
        Task<Author?> GetAuthorByBookId(int bookId);

    }
}
