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
        readonly List<string> _pendingLogMessages;

        protected BusinessObjectRepository()
        {
            _pendingLogMessages = new List<string>();
        }


        public void SetDirty(object invoker)
        {
            if (!_pendingTransactionId.HasValue)
            {
                _pendingTransactionId = Guid.NewGuid();
            }
        }

        public abstract bool IsReadonly { get; }


        protected Guid? GetTransactionID()
        {
            return _pendingTransactionId;
        }


        public void AddPendingLogMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _pendingLogMessages.Add(message);
            }
        }


        protected virtual void WriteLogMessage(string message)
        {
        }


        protected void FlushLogMessages()
        {
            foreach (var message in _pendingLogMessages)
            {
                WriteLogMessage(message);
            }
        }


        public abstract Guid? SaveChanges();

        public abstract Task<Guid?> SaveChangesAsync();
    }
}
