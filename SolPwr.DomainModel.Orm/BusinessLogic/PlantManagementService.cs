using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.Dto;
using OnionDlx.SolPwr.Persistence;
using OnionDlx.SolPwr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessLogic
{
    internal class PlantManagementService : IPlantManagementService
    {
        readonly ContextUoW _uow;
        readonly string _connString;
        readonly ILogger<IPlantManagementService> _logger;

        public async Task<PlantMgmtResponse> CreatePlantAsync(PowerPlant dtoRegister)
        {
            // Bump the database
            return await _uow.ExecuteCommandWithId(context =>
            {
                var plant = new BusinessObjects.PowerPlant
                {
                    Id = Guid.NewGuid(),
                    UtcInstallDate = dtoRegister.UtcInstallDate,
                    PlantName = dtoRegister.PlantName,
                    PowerCapacity = dtoRegister.PowerCapacity,
                    Location = dtoRegister.Location
                };

                context.PowerPlants.Add(plant);
                return plant.Id;
            });

            // TODO
            // Make sure we start seeding power data
        }


        public async Task<PlantMgmtResponse> UpdatePlantAsync(Guid identity, PowerPlant dtoRegister)
        {
            return await _uow.ExecuteCommand(context =>
            {
                var target = from plant in context.PowerPlants where plant.Id == identity select plant;
                if (!target.Any())
                {
                    return PlantMgmtResponse.CreateFaulted("Plant not found");
                }

                var modify = target.First();
                modify.UtcInstallDate = dtoRegister.UtcInstallDate;
                modify.PlantName = dtoRegister.PlantName;
                modify.PowerCapacity = dtoRegister.PowerCapacity;
                modify.Location = dtoRegister.Location;
                return null;
            });
        }


        public async Task<PlantMgmtResponse> DeletePlantAsync(Guid identity)
        {
            return await _uow.ExecuteCommand(context =>
            {
                var target = from plant in context.PowerPlants where plant.Id == identity select plant;
                if (!target.Any())
                {
                    return PlantMgmtResponse.CreateFaulted("Plant not found");
                }

                var modify = target.First();
                var history = from rec in context.GenerationHistory where rec.PowerPlant.Id == identity select rec;
                context.GenerationHistory.RemoveRange(history);
                context.PowerPlants.Remove(modify);
                return null;
            });
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


        public async Task<PlantMgmtResponse> SeedPlantsAsync(int quartersBehind)
        {
            // We only go back in time
            if (quartersBehind <= 0)
            {
                return PlantMgmtResponse.CreateFaulted("Invalid seed value");
            }

            return await _uow.ExecuteCommand(context =>
            {
                var now = DateTime.UtcNow;
                foreach (var plant in context.PowerPlants)
                {
                    for (int i = quartersBehind; i > 0; i--)
                    {
                        var totalMinutes = 15 * i;
                        var historyRec = new BusinessObjects.PowerGenerationRecord
                        {
                            Id = Guid.NewGuid(),
                            PowerPlant = plant,
                            UtcTimestamp = now.AddMinutes(-totalMinutes),
                            PowerGenerated = Random.Shared.Next(0, 100) * plant.PowerCapacity / 100.0
                        };

                        context.GenerationHistory.Add(historyRec);
                    }
                }
                return null;
            });
        }


        public Task<IEnumerable<PowerPlantImmutable>> GetForecastsAsync()
        {
            throw new NotImplementedException();
        }


        public PlantManagementService(string connString, ILogger<IPlantManagementService> logger)
        {
            _connString = connString;
            _logger = logger;

            _uow = new ContextUoW(logger, connString);
        }
    }


}
