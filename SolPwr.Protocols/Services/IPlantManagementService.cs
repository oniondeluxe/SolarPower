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
        Task<IEnumerable<PowerPlantImmutable>> GetAllPlants();

        Task<PlantCrudResponse> CreatePlantAsync(PowerPlant dtoRegister);

        Task<PlantCrudResponse> UpdatePlantAsync(Guid identity, PowerPlant dtoRegister);

        Task<PlantCrudResponse> DeletePlantAsync(Guid identity);
    }
}
