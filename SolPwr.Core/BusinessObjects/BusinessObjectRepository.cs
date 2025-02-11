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

        protected virtual Guid? GetTransactionID()
        {
            return null;
        }


        public abstract Guid? SaveChanges();

        public abstract Task<Guid?> SaveChangesAsync();
    }
}
