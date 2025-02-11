﻿using Microsoft.Extensions.Logging;
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
    /// <summary>
    /// A very simplified unit of work. Making sure everything is handled uniformly, including logging
    /// </summary>
    internal class ContextUoW
    {
        readonly ILogger<IPlantManagementService> _logger;
        readonly string _connString;


        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(Func<UtilitiesContext, Task<IEnumerable<T>>> onExecute)
        {
            try
            {
                using (var context = new UtilitiesContext(_connString))
                {
                    return await onExecute(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Array.Empty<T>();
            }
        }


        public async Task<PlantMgmtResponse> ExecuteCommandAsync(Func<UtilitiesContext, CommandResult<PlantMgmtResponse>> onExecute)
        {
            try
            {
                using (var context = new UtilitiesContext(_connString))
                {
                    var errResponse = onExecute(context);
                    if (errResponse != null)
                    {
                        if (!errResponse.PendingChanges)
                        {
                            return errResponse.Payload;
                        }

                        await context.SaveChangesAsync();
                        return errResponse.Payload;
                    }
                }

                return PlantMgmtResponse.CreateSuccess("OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return PlantMgmtResponse.CreateFaulted(ex.Message);
            }
        }


        public ContextUoW(ILogger<IPlantManagementService> logger, string connStr)
        {
            _logger = logger;
            _connString = connStr;
        }
    }
}
