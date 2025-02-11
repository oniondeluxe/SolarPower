using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    public interface IBusinessObjectCollection<T> : IEnumerable<T>, IAsyncEnumerable<T>
        where T : IBusinessObject
    {
        void Add(T obj);

        void Remove(T obj);

        void RemoveRange(IEnumerable<T> obj);
    }
}
