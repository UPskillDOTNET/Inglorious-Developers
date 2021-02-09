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
    public class CentralParkContext : DbContext
    {
        public CentralParkContext(DbContextOptions<CentralParkContext> options) : base(options) { }

        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<CentralReservation> CentralReservations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
