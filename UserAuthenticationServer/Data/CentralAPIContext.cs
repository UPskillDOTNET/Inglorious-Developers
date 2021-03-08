using CentralAPI.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Data
{
    public class CentralAPIContext : IdentityDbContext<User>
    {
        public CentralAPIContext(DbContextOptions<CentralAPIContext> options) : base(options)
        {
        }

  

    }
}


