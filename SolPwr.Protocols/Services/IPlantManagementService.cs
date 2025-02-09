﻿using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    public interface IPlantManagementService
    {
        Task<IEnumerable<PowerPlant>> GetAllPlants();

        Task<PlantCrudResponse> CreatePlantAsync(PowerPlant dtoRegister);        
    }
}
