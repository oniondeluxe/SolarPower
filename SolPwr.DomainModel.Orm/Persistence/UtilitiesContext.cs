using Microsoft.EntityFrameworkCore;
using OnionDlx.SolPwr.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Persistence
{
    internal class UtilitiesContext : DbContext
    {
        public DbSet<PowerPlant> PowerPlants { get; set; }

        public DbSet<PowerGenerationRecord> GenerationHistory { get; set; }

        #region Setup

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("spa");

            // PowerPlant geo data columns
            modelBuilder.ApplyConfiguration(new PowerPlantConfiguration());
        }

        #endregion

        public UtilitiesContext(DbContextOptions options) : base(options)
        {
        }
    }
}
