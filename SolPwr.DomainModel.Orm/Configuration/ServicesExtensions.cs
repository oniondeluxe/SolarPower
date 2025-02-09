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
        public Task<PlantCrudResponse> CreatePlantAsync(PowerPlant dtoRegister)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PowerPlant>> GetAllPlants()
        {
            var result = new List<PowerPlant>
            {
                new PowerPlant {  PlantName = "Plant 1", PowerCapacity = 1000, Location = new Data.GeoCoordinate(10.0, 20.0) },
                new PowerPlant {  PlantName = "Plant 2", PowerCapacity = 2000, Location = new Data.GeoCoordinate(11.0, 21.0) },
                new PowerPlant {  PlantName = "Plant 3", PowerCapacity = 3000, Location = new Data.GeoCoordinate(12.0, 22.0) },
            };

            return Task.FromResult(result.AsEnumerable());
        }

        public TEMP_TEST()
        {

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

            coll.AddScoped<IPlantManagementService, TEMP_TEST>();

            return coll;
        }
    }
}
