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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LibraryContext libraryContext) : base(libraryContext) { }

        public async Task<User> GetByCreds(string username, string password)
        {
            return await _libraryContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _libraryContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
