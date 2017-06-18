using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models
{
    public class NumberTumblerContext : DbContext
    {
        //IConfigurationRoot _config;

        public NumberTumblerContext(DbContextOptions options) : base(options)
        {
            //_config = config;
        }

        public DbSet<NumberSet> NumberSets { get; set; }
        public DbSet<NumberSetNumber> NumberSetNumbers { get; set; }
        public DbSet<Shuffle> Shuffles { get; set; }
        public DbSet<ShuffleNumber> ShuffleNumbers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;initial catalog=NumberTumblerDb;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NumberSet>()
             .HasMany(c => c.NumberSetNumbers)
             .WithOne(e => e.NumberSet);

            modelBuilder.Entity<NumberSetNumber>()
             .HasIndex(nsn => new { nsn.NumberSetID, nsn.Number })
             .IsUnique(true);

            modelBuilder.Entity<Shuffle>()
            .HasMany(c => c.ShuffleNumbers)
            .WithOne(e => e.Shuffle);

            modelBuilder.Entity<ShuffleNumber>()
             .HasIndex(nsn => new { nsn.ShuffleID, nsn.Number })
             .IsUnique(true);
        }
    }
}
