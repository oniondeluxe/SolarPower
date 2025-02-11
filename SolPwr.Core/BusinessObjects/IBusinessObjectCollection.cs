using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    public interface IBusinessObjectCollection<out T> : IEnumerable<T>, IAsyncEnumerable<T>
        where T : IBusinessObject
    {
        // Not used
    }
}
