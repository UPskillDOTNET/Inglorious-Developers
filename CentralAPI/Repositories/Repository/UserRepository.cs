﻿using CentralAPI.Data;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<User> GetUsersById(string id)
        {
            return await GetAll().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<User> UpdateUserById(string id, User user)
        {
            await UpdateAsync(user);
            return user;
        }

        public async Task<User> CreateUser(User user)
        {
           
            user = await AddAsync(user);

            return user;
        }

        public async Task<User> DeleteUserProfile(string id)
        {
            var user = GetAll().FirstOrDefault(u => u.Id == id);

            await DeleteAsync(user);

            return user;
        }
    }
}
