using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Dto
{
    public class UserAccount : IDataTransferObject
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public int SystemRole { get; set; }
    }
}
