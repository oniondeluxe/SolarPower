using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Dto
{
    public class PlantMgmtResponse : IDataTransferObject
    {
        public Guid? Id { get; set; }

        public bool Success { get; set; }   

        public string Message { get; set; }

        public IEnumerable<(string, string)> ErrorInfo { get; set; }

        public static PlantMgmtResponse CreateSuccess(string message)
        {
            return new PlantMgmtResponse
            {
                Success = true,
                Message = message
            };
        }


        public static PlantMgmtResponse CreateFaulted(string message)
        {
            return new PlantMgmtResponse
            {
                Success = false,
                Message = message
            };
        }


        public PlantMgmtResponse WithId(Guid id)
        {
            Id = id;
            return this;
        }


        public PlantMgmtResponse()
        {
            ErrorInfo = Array.Empty<(string, string)>();
        }
    }
}
