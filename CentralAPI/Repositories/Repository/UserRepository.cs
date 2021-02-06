using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CentralAPIContext CentralAPIContext) : base(CentralAPIContext)
        {
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await GetAll().ToListAsync();
        }
    }
}
