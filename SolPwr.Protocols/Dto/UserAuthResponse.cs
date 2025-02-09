using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Dto
{
    public class UserAuthResponse : IDataTransferObject
    {
        public bool Success { get; set; }   

        public string Message { get; set; }

        public IEnumerable<(string, string)> ErrorInfo { get; set; }

        public static UserAuthResponse CreateSuccess(string message)
        {
            return new UserAuthResponse
            {
                Success = true,
                Message = message
            };
        }


        public static UserAuthResponse CreateFaulted(string message)
        {
            return new UserAuthResponse
            {
                Success = false,
                Message = message
            };
        }


        public UserAuthResponse()
        {
            ErrorInfo = Array.Empty<(string, string)>();
        }
    }
}
