using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Commands
{
    public interface ICommandRunner
    {
        Task Execute<TEntity>(ICommand<TEntity> command, TEntity entity);
        Task<TResult> Execute<TEntity, TResult>(ICommand<TEntity, TResult> command, TEntity entity);
        IList<string> Validate<TEntity>(ICommand<TEntity> command, TEntity entity);
    }
}
