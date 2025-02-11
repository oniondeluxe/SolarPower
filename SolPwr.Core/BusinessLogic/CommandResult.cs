using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessLogic
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



    public abstract class CommandResultOLD
    {
        public abstract bool PendingChanges { get; }

        public static CommandResultOLD<T> Create<T>(T payload) where T : ITransactionalDto
        {
            return new CommandResultOLD<T>(payload);
        }
    }


    public class CommandResultOLD<T> : CommandResultOLD where T : ITransactionalDto
    {
        public T Payload { get; init; }

        public override bool PendingChanges
        {
            get
            {
                return Payload.TransactionId.HasValue;
            }
        }

        internal CommandResultOLD(T payload)
        {
            Payload = payload;
        }
    }
}
