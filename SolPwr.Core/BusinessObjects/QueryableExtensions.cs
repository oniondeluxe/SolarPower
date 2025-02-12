using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    public interface IConverter
    {
        Task<T> GetFirstOrDefaultAsync<T>(IQueryable<T> coll) where T : IBusinessObject;

        Task<List<T>> GetToListAsync<T>(IQueryable<T> coll) where T : IBusinessObject;
    }


    public class ConverterEventArgs : EventArgs
    {
        public IConverter Converter { get; set; }
    }


    public static class Extensions
    {
        public static event EventHandler<ConverterEventArgs> RequestConvert;

        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> coll) where T : IBusinessObject
        {
            if (RequestConvert != null)
            {
                var retVal = new ConverterEventArgs();
                RequestConvert(null, retVal);
                if (retVal.Converter != null)
                {
                    return retVal.Converter.GetFirstOrDefaultAsync(coll);
                }
            }

            return Task.FromResult<T>(default);
        }


        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> coll) where T : IBusinessObject
        {
            if (RequestConvert != null)
            {
                var retVal = new ConverterEventArgs();
                RequestConvert(null, retVal);
                if (retVal.Converter != null)
                {
                    return retVal.Converter.GetToListAsync(coll);
                }
            }

            return Task.FromResult<List<T>>(default);
        }
    }
}
