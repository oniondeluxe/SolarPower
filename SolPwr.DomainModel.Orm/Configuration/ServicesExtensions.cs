using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnionDlx.SolPwr.Dto;
using OnionDlx.SolPwr.Persistence;
using OnionDlx.SolPwr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Configuration
{
    // TODO: Remove reference to Protocols again
    class TEMP_TEST : IPlantManagementService
    {
        readonly string _connString;

        public Task<PlantMgmtResponse> CreatePlantAsync(PowerPlant dtoRegister)
        {
            return Task.FromResult(PlantMgmtResponse.CreateSuccess("OK"));
        }


        public Task<PlantMgmtResponse> UpdatePlantAsync(Guid identity, PowerPlant dtoRegister)
        {
            return Task.FromResult(PlantMgmtResponse.CreateSuccess("OK"));
        }


        public Task<PlantMgmtResponse> DeletePlantAsync(Guid identity)
        {
            return Task.FromResult(PlantMgmtResponse.CreateSuccess("OK"));
        }


        public async Task<IEnumerable<PowerPlantImmutable>> GetAllPlantsAsync()
        {
            var result = new List<PowerPlantImmutable>();
            using (var context = new UtilitiesContext(_connString))
            {
                var result1 = await context.PowerPlants.ToListAsync();
                foreach (var dbRecord in result1)
                {
                    result.Add(new PowerPlantImmutable
                    {
                        Id = dbRecord.Id,
                        UtcInstallDate = dbRecord.UtcInstallDate,
                        PlantName = dbRecord.PlantName,
                        PowerCapacity = dbRecord.PowerCapacity,
                        Location = dbRecord.Location
                    });
                }
            }

            return result;
        }


        public Task<PlantMgmtResponse> SeedPlantsAsync()
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<PowerPlantImmutable>> GetForecastsAsync()
        {
            throw new NotImplementedException();
        }


        public TEMP_TEST(string connString)
        {
            _connString = connString;
        }
    }






    public static class ServicesDomainExtensions
    {
        /// <summary>
        /// Will add the needed boilerplate to IoC, without the need to introduce a dependency to EF in the main app
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static IServiceCollection AddPersistence(this IServiceCollection coll, string connString)
        {
            coll.AddDbContext<UtilitiesContext>(options => options.UseSqlServer(connString));

            //coll.AddScoped<IPlantManagementService, TEMP_TEST>();
            coll.AddScoped<IPlantManagementService>(c => new TEMP_TEST(connString));

            return coll;
        }
    }
}
