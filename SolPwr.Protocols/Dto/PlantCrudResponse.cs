using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Dto
{
    public class PlantCrudResponse : IDataTransferObject
    {
        public bool Success { get; set; }   

        public string Message { get; set; }

        public IEnumerable<(string, string)> ErrorInfo { get; set; }

        public static PlantCrudResponse CreateSuccess(string message)
        {
            return new PlantCrudResponse
            {
                Success = true,
                Message = message
            };
        }


        public static PlantCrudResponse CreateFaulted(string message)
        {
            return new PlantCrudResponse
            {
                Success = false,
                Message = message
            };
        }


        public PlantCrudResponse()
        {
            ErrorInfo = Array.Empty<(string, string)>();
        }
    }
}
