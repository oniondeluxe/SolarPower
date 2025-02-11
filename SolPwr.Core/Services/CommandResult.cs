using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    public abstract class CommandResult
    {
        public abstract bool PendingChanges { get; set; }

        public static CommandResult<T> Create<T>(T payload, bool commit = false) where T : IDataTransferObject
        {
            return new CommandResult<T>(payload, commit);
        }
    }


    public class CommandResult<T> : CommandResult where T : IDataTransferObject
    {
        public override bool PendingChanges { get; set; }

        public T Payload { get; init; }

        internal CommandResult(T payload, bool pendingChanges)
        {
            Payload = payload;
            PendingChanges = pendingChanges;
        }
    }
}
