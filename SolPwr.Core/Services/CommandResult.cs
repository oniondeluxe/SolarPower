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
        public abstract bool PendingChanges { get; }

        public static CommandResult<T> Create<T>(T payload) where T : ITransactionalDto
        {
            return new CommandResult<T>(payload);
        }
    }


    public class CommandResult<T> : CommandResult where T : ITransactionalDto
    {
        public T Payload { get; init; }

        public override bool PendingChanges
        {
            get
            {
                return Payload.TransactionId.HasValue;
            }
        }

        internal CommandResult(T payload)
        {
            Payload = payload;
        }
    }
}
