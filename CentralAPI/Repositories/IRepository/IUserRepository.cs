using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
       Task<IEnumerable<User>> GetUsers();

       Task<User> GetUsersById(string id);
       
       Task<User> UpdateUserById(string id, User user);

       Task<User> CreateUser(User user);

       Task<User> DeleteUserProfile(string id);
    }
}
