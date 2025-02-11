using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    internal class EntityCollection<T> : IBusinessObjectCollection<T> where T : class, IBusinessObject
    {
        readonly ILogger<IUtilitiesRepositoryFactory> _logger;
        readonly DbSet<T> _dataSet;

        public EntityCollection(DbSet<T> dataSet, ILogger<IUtilitiesRepositoryFactory> logger)
        {
            _dataSet = dataSet;
            _logger = logger;
        }

        
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return _dataSet.GetAsyncEnumerator(cancellationToken);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _dataSet.AsEnumerable().GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
