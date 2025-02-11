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


        private static UnitOfWork<T> Create<T>(IRepositoryFactory<T> input)
            where T : IBusinessObjectRepository
        {
            return null;
        }


        public static void ExecuteCommand(Action onExecute)
        {
        }
    }


    public class UnitOfWork<T> : UnitOfWork
    {
        public UnitOfWork<T, D> For<D>() where D : ITransactionalDto
        {
            return null;
        }

        internal UnitOfWork()
        {

        }
    }


    public class UnitOfWork<T, D> : UnitOfWork<T> where D : ITransactionalDto
    {
        internal UnitOfWork()
        {            
        }

        public Task<D> ExecuteCommandAsync(Func<T, CommandFactory, object> onExecute)
        {
            return null;
        }
    }


    public static class UnitOfWorkExtensions
    {
        //public static Task ExecuteCommandAsync<T>(this IRepositoryFactory<T> source, Action onExecute)
        //    where T : IBusinessObjectRepository
        //{
        //    return null;
        //}


        public static UnitOfWork<T> CreateUow<T>(this IRepositoryFactory<T> input, ILogger logger)
         where T : IBusinessObjectRepository
        {
            return null;
        }
    }


    public class CommandFactory
    {
        public object AsSuccessful(bool commit = false)
        {
            return null;
        }


        public object AsSuccessful(string transactionErrorMessage)
        {
            return null;
        }


        public object AsFaulted(string transactionErrorMessage)
        {
            return null;
        }
    }
}
