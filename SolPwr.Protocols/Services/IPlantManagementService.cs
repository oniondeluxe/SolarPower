using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    public interface IPlantManagementService
    {
        Task<IEnumerable<PowerPlantImmutable>> GetAllPlantsAsync();

        Task<PlantMgmtResponse> SeedPlantsAsync(int daysBehind);

        Task<PlantMgmtResponse> CreatePlantAsync(PowerPlant dtoRegister);

        Task<PlantMgmtResponse> UpdatePlantAsync(Guid identity, PowerPlant dtoRegister);

        Task<PlantMgmtResponse> DeletePlantAsync(Guid identity);

        Task<IEnumerable<PowerPlantImmutable>> GetForecastsAsync();
    }
}
