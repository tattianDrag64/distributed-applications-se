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
    public class AuthorRepository : IAuthorRepository, IRepository<Author>
    {
        public Author Add(Author obj)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> AddRange(IEnumerable<Author> obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> FindAsync(Expression<Func<Author, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author?> GetAuthorWithBooksAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<Author?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Author entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
