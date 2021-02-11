using CentralAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Data
{
    public class CentralAPIContext : DbContext
    {
        public CentralAPIContext(DbContextOptions<CentralAPIContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Sublet> Sublets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReservationPayment> ReservationPayments { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<CentralReservation> CentralReservations { get; set; }

    }
}


