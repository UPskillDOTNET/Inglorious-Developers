using CentralAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Data
{
    public class CentralParkContext :IdentityDbContext<apiUser>
    {
        public CentralParkContext(DbContextOptions<CentralParkContext> options) : base(options) { }

        public DbSet<ParkingLot> ParkingLots { get; set; }
        //public DbSet<Reservation> Reservations { get; set; }
    }
}
