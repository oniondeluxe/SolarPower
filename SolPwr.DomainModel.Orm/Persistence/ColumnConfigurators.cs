using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnionDlx.SolPwr.BusinessObjects;
using OnionDlx.SolPwr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Persistence
{
    public class PowerPlantConfiguration : IEntityTypeConfiguration<PowerPlant>
    {
        public void Configure(EntityTypeBuilder<PowerPlant> builder)
        {
            builder.OwnsOne(p => p.Location, a =>
            {
                a.Property(p => p.Latitude).HasColumnName("Latitude");
                a.Property(p => p.Longitude).HasColumnName("Longitude");
            });
        }

        public PowerPlantConfiguration()
        {            
        }
    }
}
