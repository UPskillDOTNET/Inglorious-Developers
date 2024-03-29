﻿using Microsoft.EntityFrameworkCore;

using PrivateParkAPI.Models;


namespace PrivateParkAPI.Data
{
    public class PrivateParkContext : DbContext
    {
        public PrivateParkContext(DbContextOptions<PrivateParkContext> options) : base(options)
        {

        }

        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
