using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.Data;
using OnionDlx.SolPwr.Dto;
using OnionDlx.SolPwr.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessLogic
{
    internal class PlantManagementService : IPlantManagementService
    {
        readonly BusinessObjects.IUtilitiesRepositoryFactory _repoFac;
        readonly ILogger<IPlantManagementService> _logger;
        readonly IMeteoLookupServiceCallback _meteoCallback;

        public //async 
            Task<PlantMgmtResponse> CreatePlantAsync(PowerPlant dtoRegister)
        {
            //// Bump the database
            //var plantId = await _uow.ExecuteCommandAsync(context =>
            //{
            //    var plant = new BusinessObjects.PowerPlant
            //    {
            //        Id = Guid.NewGuid(),
            //        UtcInstallDate = dtoRegister.UtcInstallDate,
            //        PlantName = dtoRegister.PlantName,
            //        PowerCapacity = dtoRegister.PowerCapacity,
            //        Location = dtoRegister.Location
            //    };

            //    context.PowerPlants.Add(plant);
            //    return CommandResult.Create(PlantMgmtResponse.CreateSuccess("OK", Guid.NewGuid()).WithId(plant.Id), true);
            //});

            //// Make sure we start feeding power data in the worker thread
            //await _meteoCallback.GetEndpoint()?.StartFeedAsync(plantId.Id.Value, dtoRegister.Location);

            //return plantId;

            return Task.FromResult<PlantMgmtResponse>(null);
        }


        public //async 
            Task<PlantMgmtResponse> UpdatePlantAsync(Guid identity, PowerPlant dtoRegister)
        {
            //return await _uow.ExecuteCommandAsync(context =>
            //{
            //    var target = from plant in context.PowerPlants where plant.Id == identity select plant;
            //    if (!target.Any())
            //    {
            //        // From an API perspective, this is a success (idempotent operation)
            //        return CommandResult.Create(PlantMgmtResponse.CreateSuccess("Plant not found"));
            //    }

            //    var modify = target.First();
            //    modify.UtcInstallDate = dtoRegister.UtcInstallDate;
            //    modify.PlantName = dtoRegister.PlantName;
            //    modify.PowerCapacity = dtoRegister.PowerCapacity;
            //    modify.Location = dtoRegister.Location;

            //    return CommandResult.Create(PlantMgmtResponse.CreateSuccess(identity.ToString(), Guid.NewGuid()), true);
            //});

            return Task.FromResult<PlantMgmtResponse>(null);
        }


        public // async 
            Task<PlantMgmtResponse> DeletePlantAsync(Guid identity)
        {
            //return await _uow.ExecuteCommandAsync(context =>
            //{
            //    var target = from plant in context.PowerPlants where plant.Id == identity select plant;
            //    if (!target.Any())
            //    {
            //        // From an API perspective, this is a success (idempotent operation)
            //        return CommandResult.Create(PlantMgmtResponse.CreateSuccess("Plant not found"));
            //    }

            //    var modify = target.First();
            //    var history = from rec in context.GenerationHistory where rec.PowerPlant.Id == identity select rec;
            //    context.GenerationHistory.RemoveRange(history);
            //    context.PowerPlants.Remove(modify);

            //    return CommandResult.Create(PlantMgmtResponse.CreateSuccess(identity.ToString(), Guid.NewGuid()), true);
            //});

            return Task.FromResult<PlantMgmtResponse>(null);
        }


        public Task<IEnumerable<PowerPlantImmutable>> GetAllPlantsAsync()
        {
            //return _uow.ExecuteQueryAsync<PowerPlantImmutable>(async context =>
            //{
            //    var result = new List<PowerPlantImmutable>();
            //    await foreach (var dbRecord in context.PowerPlants)
            //    {
            //        result.Add(new PowerPlantImmutable
            //        {
            //            Id = dbRecord.Id,
            //            UtcInstallDate = dbRecord.UtcInstallDate,
            //            PlantName = dbRecord.PlantName,
            //            PowerCapacity = dbRecord.PowerCapacity,
            //            Location = dbRecord.Location
            //        });
            //    }
            //    return result;
            //});

            return Task.FromResult<IEnumerable<PowerPlantImmutable>>(Array.Empty<PowerPlantImmutable>());
        }


        private void OnUpdateProductionData(IMeteoLookupService meteo)
        {
            //var now = DateTime.UtcNow;
            //var tempStorage = new List<(Guid, IList<MeteoData>)>();
            //using (var context = new UtilitiesContext(_connString))
            //{
            //    // Go through all plants and find their location etc
            //    var plants = from p in context.PowerPlants select p;
            //    foreach (var plant in plants)
            //    {
            //        var mdList = new List<MeteoData>();
            //        // Zero values means now
            //        var data = meteo.GetMeteoDataAsync(plant.Location, TimeResolution.None, TimeSpanCode.None, 0).Result;
            //        mdList.AddRange(data);
            //        tempStorage.Add((plant.Id, mdList));
            //    }
            //}

            //// Now, update the DB with these added rows
            //foreach (var item in tempStorage)
            //{
            //    // TODO: Insert rows
            //}
        }


        public //async 
            Task<PlantMgmtResponse> SeedPlantsAsync(int quartersBehind)
        {
            //// We only go back in time
            //if (quartersBehind <= 0)
            //{
            //    return PlantMgmtResponse.CreateFaulted("Invalid seed value");
            //}

            //return await _uow.ExecuteCommandAsync(context =>
            //{
            //    var counter = 0;
            //    var now = DateTime.UtcNow;
            //    foreach (var plant in context.PowerPlants)
            //    {
            //        for (int i = quartersBehind; i > 0; i--)
            //        {
            //            var totalMinutes = 15 * i;
            //            var historyRec = new BusinessObjects.PowerGenerationRecord
            //            {
            //                Id = Guid.NewGuid(),
            //                PowerPlant = plant,
            //                UtcTimestamp = now.AddMinutes(-totalMinutes),
            //                PowerGenerated = Random.Shared.Next(0, 100) * plant.PowerCapacity / 100.0
            //            };

            //            context.GenerationHistory.Add(historyRec);
            //        }
            //        counter++;
            //    }

            //    return CommandResult.Create(PlantMgmtResponse.CreateSuccess(counter.ToString(), Guid.NewGuid()), true);
            //});

            return Task.FromResult<PlantMgmtResponse>(null);
        }


        public // async 
            Task<IEnumerable<PlantPowerData>> GetPowerDataAsync(Guid identity, PowerDataTypes type, TimeResolution resol, TimeSpanCode code, int timeSpan)
        {
            /*
            // History: we read from the stored data
            if (type == PowerDataTypes.History)
            {
                return await _uow.ExecuteQueryAsync<PlantPowerData>(async context =>
                {
                    // We don't include the history records in the query yet
                    var plants = from p in context.PowerPlants
                                 where p.Id == identity
                                 select p;
                    var currentPlant = await plants.FirstOrDefaultAsync();
                    if (currentPlant != null)
                    {
                        var result = new List<PlantPowerData>();
                        var history = from hist in context.GenerationHistory
                                      where hist.PowerPlant.Id == currentPlant.Id
                                      select hist;
                        foreach (var histItem in await history.ToListAsync())
                        {
                            result.Add(new PlantPowerData
                            {
                                PlantId = currentPlant.Id,
                                CurrentPower = histItem.PowerGenerated,
                                UtcTime = histItem.UtcTimestamp
                            });
                        }

                        return result;
                    }
                    else
                    {
                        return Array.Empty<PlantPowerData>();
                    }
                });
            }
            else if (type == PowerDataTypes.Forecast)
            {
                // Forecast, we ask the meteo service
                var meteo = _meteoCallback.GetEndpoint();

                // Find the plant in question first
                using (var context = new UtilitiesContext(_connString))
                {
                    var plants = from p in context.PowerPlants
                                 where p.Id == identity
                                 select p;
                    var currentPlant = await plants.FirstOrDefaultAsync();
                    if (currentPlant != null)
                    {
                        var result = new List<PlantPowerData>();

                        // Fake science - The latitude will influence how much power the sun will generate
                        var now = DateTime.UtcNow;
                        var data = await meteo.GetMeteoDataAsync(currentPlant.Location, resol, code, timeSpan);
                        foreach (var dp in data)
                        {
                            var calc = new PowerCalculator(currentPlant.PowerCapacity, currentPlant.Location.Latitude);
                            var power = calc.GetCurrentPower(dp.WeatherCode, dp.Visibility);
                            result.Add(new PlantPowerData
                            {
                                PlantId = currentPlant.Id,
                                CurrentPower = power,
                                UtcTime = dp.UtcTime
                            });
                        }

                        return result;
                    }
                }
            }
            */

            // Nothing ordered, or nothing found
            return Task.FromResult<IEnumerable<PlantPowerData>>(Array.Empty<PlantPowerData>());
        }


        public PlantManagementService(BusinessObjects.IUtilitiesRepositoryFactory repo, ILogger<IPlantManagementService> logger, IMeteoLookupServiceCallback factory)
        {
            _logger = logger;
            _repoFac = repo;

            // Subscribe to events coming from the outer worker
            _meteoCallback = factory;
            _meteoCallback.ServicePushUpdate += (sender, service) =>
            {
                // The background worker will invoke this, but WE have to take care if it, using the 
                // caller, to ask for data from the Internet
                OnUpdateProductionData(service);
            };
        }
    }
}
