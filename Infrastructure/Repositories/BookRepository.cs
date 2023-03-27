using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext libraryContext) : base(libraryContext) { }

        public async Task<Book> GetByISBNAsync(string isbn)
        {
            return await _libraryContext.Set<Book>()
                .FirstOrDefaultAsync(x => x.ISBN == isbn);
        }
    }
}
