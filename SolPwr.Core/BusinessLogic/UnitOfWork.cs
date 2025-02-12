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


        public async Task<D> ExecuteCommandAsync(Func<T, CommandResultFactory<D>, CommandResult<D>> onExecute, Action<D> onPopulateResponse = null)
        {
            D response = default;
            try
            {
                using (var repo = _input.NewCommand())
                {
                    response = _factory.CreateInstance(null);
                    var commandResultFactory = new CommandResultFactory<D>(response);
                    var result = onExecute(repo, commandResultFactory);
                    if (result.PendingChanges)
                    {
                        // As we have pending changes, we will also get a transaction ID
                        var trx = await repo.SaveChangesAsync();
                        response.TransactionId = trx;

                        if (trx.HasValue)
                        {
                            _logger.LogInformation(trx.Value.ToString());
                        }
                    }
                    if (onPopulateResponse != null)
                    {
                        onPopulateResponse(response);
                    }
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = _factory.CreateInstance(null);
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
