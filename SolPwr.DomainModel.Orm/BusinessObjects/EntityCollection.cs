using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    internal class EntityCollection<T> : IBusinessObjectCollection<T> where T : class, IBusinessObject
    {
        readonly DbSet<T> _dataSet;
        readonly BusinessObjectRepository _owner;

        public EntityCollection(DbSet<T> dataSet, BusinessObjectRepository owner)
        {
            _dataSet = dataSet;
            _owner = owner;
        }


        public void Add(T obj)
        {
            _dataSet.Add(obj);

            // Logging will only take place when we know we are saving
            _owner.AddPendingLogMessage($"Added '{obj.Id}'");
            _owner.SetDirty(this);
        }


        public void Remove(T obj)
        {
            _dataSet.Remove(obj);

            // Logging will only take place when we know we are saving
            _owner.AddPendingLogMessage($"Removed '{obj.Id}'");
            _owner.SetDirty(this);
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
