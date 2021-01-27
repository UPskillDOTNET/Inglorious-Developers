using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Authentication;
using PrivateParkAPI.Models;


namespace PrivateParkAPI.Data
{
    public class PrivateParkContext : IdentityDbContext<apiUser>
    {
        public PrivateParkContext(DbContextOptions<PrivateParkContext> options) : base(options)
        {

        }

        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
