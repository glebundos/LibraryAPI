using Core.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryContext _libraryContext;

        public Repository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _libraryContext.Set<T>().AddAsync(entity);
            await _libraryContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _libraryContext.Set<T>().Remove(entity);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _libraryContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _libraryContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _libraryContext.SetModified(entity);
            await _libraryContext.SaveChangesAsync();
            return entity;
        }
    }
}
