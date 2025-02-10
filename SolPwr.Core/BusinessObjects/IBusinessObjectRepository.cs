using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    public interface IBusinessObjectRepository<T> : IEnumerable<T>
        where T : IBusinessObject
    {
        // Not used
    }
}
