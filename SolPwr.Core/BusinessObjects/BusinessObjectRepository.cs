using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    public abstract class BusinessObjectRepository : IBusinessObjectRepository
    {
        #region IDisposable

        protected bool disposed = false;

        ~BusinessObjectRepository()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                // TODO
                disposed = true;
            }
        }

        #endregion

        Guid? _pendingTransactionId;

        public void SetDirty(object invoker)
        {
            if (!_pendingTransactionId.HasValue)
            {
                _pendingTransactionId = Guid.NewGuid();
            }
        }


        protected virtual Guid? GetTransactionID()
        {
            return _pendingTransactionId;
        }


        public void AddPendingLogMessage(string message)
        {

        }


        protected void FlushLogMessages()
        {
        }


        public abstract Guid? SaveChanges();

        public abstract Task<Guid?> SaveChangesAsync();
    }
}
