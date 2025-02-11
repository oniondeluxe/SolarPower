using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.BusinessObjects;
using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessLogic
{
    public abstract class UnitOfWork
    {
        protected UnitOfWork()
        {
        }
    }


    public class DtoFactory<D> where D : ITransactionalDto
    {
        internal D CreateInstance(Guid? transactionId)
        {
            return default;
        }


        public DtoFactory()
        {            
        }
    }


    public class UnitOfWorkTemplate<T> where T : IBusinessObjectRepository
    {        
        readonly IRepositoryFactory<T> _input;
        readonly ILogger _logger;

        public UnitOfWork<T, D> For<D>(DtoFactory<D> factory) where D : ITransactionalDto
        {
            return new UnitOfWork<T, D>(factory, this, _input, _logger);
        }


        internal UnitOfWorkTemplate(IRepositoryFactory<T> input, ILogger logger)
        {
            _input = input;
            _logger = logger;
        }
    }


    public class UnitOfWork<T, D> : UnitOfWork
        where D : ITransactionalDto
        where T : IBusinessObjectRepository
    {
        readonly DtoFactory<D> _factory;
        readonly UnitOfWorkTemplate<T> _creator;
        readonly IRepositoryFactory<T> _input;
        readonly ILogger _logger;

        internal UnitOfWork(DtoFactory<D> factory, UnitOfWorkTemplate<T> creator, IRepositoryFactory<T> input, ILogger logger)
        {
            _factory = factory;
            _creator = creator;
            _logger = logger;
            _input = input;
        }


        public async Task<D> ExecuteCommandAsync(Func<T, CommandFactory<D>, CommandResult<D>> onExecute, Action<D> onPopulateResponse = null)
        {
            D response = default;
            try
            {
                using (var repo = _input.NewCommand())
                {
                    var commandResultFactory = new CommandFactory<D>();
                    var result = onExecute(repo, commandResultFactory);

                    var trx = await repo.SaveChangesAsync();
                    response = _factory.CreateInstance(trx);

                    // ID
                    // Skapa Dto
                    // Commit/Ej commit
                    // Transaction ID


                    //return Dto.PlantMgmtResponse.CreateSuccess("OK", trx).WithId(plant.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return response;
        }
    }


    public static class UnitOfWorkExtensions
    {
        public static UnitOfWorkTemplate<T> CreateUow<T>(this IRepositoryFactory<T> input, ILogger logger)
         where T : IBusinessObjectRepository
        {
            return new UnitOfWorkTemplate<T>(input, logger);
        }
    }


    public class CommandFactory<D> where D : ITransactionalDto
    {
        public CommandResult<D> AsSuccessful(bool commit = false)
        {
            return null; // $"Success {commit}";
        }


        public CommandResult<D> AsSuccessful(string transactionMessage, bool commit = false)
        {
            return null; // $"Success: {transactionMessage} {commit}";
        }


        public CommandResult<D> AsFaulted(string transactionErrorMessage)
        {
            return null; // $"Error";
        }

        internal CommandFactory()
        {
            
        }
    }
}
