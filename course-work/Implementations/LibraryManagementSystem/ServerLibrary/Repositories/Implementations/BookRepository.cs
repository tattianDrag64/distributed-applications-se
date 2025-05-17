using BaseLibrary.Entities;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        public Book Add(Book obj)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Book entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> AddRange(IEnumerable<Book> obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooksByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooksWithAuthorsGenresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
