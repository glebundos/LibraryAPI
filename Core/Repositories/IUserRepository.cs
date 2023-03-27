using Core.Entities;
using Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByCreds(string username, string password);

        public Task<User> GetByUsername(string username);
    }
}
