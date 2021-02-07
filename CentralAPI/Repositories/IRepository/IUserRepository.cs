﻿using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetUsers();
    }
}