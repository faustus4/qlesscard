using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLessTransportCard.Models;
using Microsoft.EntityFrameworkCore;

namespace QLessTransportCard.Data
{
    public class QLessCardContext : DbContext
    {

        public QLessCardContext(DbContextOptions<QLessCardContext> options) : base(options)
        {
        }

        public DbSet<CardReload> CardReloads { get; set; }
        public DbSet<CustomerID> CustomerIDs { get; set; }
        public DbSet<IDType> IDTypes { get; set; }
        public DbSet<QLessCard> QLessCards { get; set; }
        public DbSet<QLessCardType> QLessCardTypes { get; set; }
        public DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardReload>().ToTable("CardReload");
            modelBuilder.Entity<CustomerID>().ToTable("CustomerID");
            modelBuilder.Entity<IDType>().ToTable("IDType");
            modelBuilder.Entity<QLessCard>().ToTable("QLessCard");
            modelBuilder.Entity<QLessCardType>().ToTable("QLessCardType");
            modelBuilder.Entity<Ride>().ToTable("Ride");
        }
    }
}
