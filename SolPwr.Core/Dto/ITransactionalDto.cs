﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Dto
{
    public interface ITransactionalDto : IDataTransferObject
    {
        Guid? TransactionId { get; set; }

        string Message { get; set;  }

        bool Success { get; set; }

        IEnumerable<(string, string)> ErrorInfo { get; set; }
    }
}
