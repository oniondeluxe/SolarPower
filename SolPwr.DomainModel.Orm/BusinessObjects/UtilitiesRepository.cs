using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    internal class UtilitiesRepositoryFactory : IUtilitiesRepositoryFactory
    {
        #region Bolierplate

        readonly string _connectionString;
        readonly ILogger<IUtilitiesRepositoryFactory> _logger;

        public UtilitiesRepositoryFactory(string connectionString, ILogger<IUtilitiesRepositoryFactory> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        #endregion    

        public IUtilitiesRepository NewCommand()
        {
            throw new NotImplementedException();
        }

        public IUtilitiesRepository NewQuery()
        {
            throw new NotImplementedException();
        }
    }



    internal class UtilitiesRepository //: IUtilitiesRepository
    {
        #region Bolierplate

        readonly string _connectionString;
        readonly ILogger<IUtilitiesRepository> _logger;

        public UtilitiesRepository(string connectionString, ILogger<IUtilitiesRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        #endregion

        public IBusinessObjectCollection<PowerPlant> PowerPlants
        {
            get
            {
                return null;
            }
        }


        public IBusinessObjectCollection<PowerGenerationRecord> GenerationRecords
        {
            get
            {
                return null;
            }
        }
    }
}
