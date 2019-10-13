using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Domain.Models;

namespace Library.Commands
{
    public class CommandRunner
    {
        private readonly LibraryContext _context;

        public CommandRunner(LibraryContext context)
        {
            _context = context;
        }

        public async Task Execute<TEntity>(ICommand<TEntity> command, TEntity entity)
        {
            await command.Execute(entity, _context);
        }

        public async Task<TResult> Execute<TEntity, TResult>(ICommand<TEntity, TResult> command, TEntity entity)
        {
            await command.Execute(entity, _context);
            return command.Result;
        }

        public IEnumerable<string> Validate<TEntity>(ICommand<TEntity> command, TEntity entity)
        {
            return command.Validate(entity, _context);
        }
    }
}
