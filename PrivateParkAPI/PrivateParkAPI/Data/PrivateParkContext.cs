using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Models;
using PrivateParkAPI.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace PrivateParkAPI.Data
{
    public class PrivateParkContext : IdentityDbContext<ApiUser>
    {
        public PrivateParkContext(DbContextOptions<PrivateParkContext> options) : base(options)
        {

        }

        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
