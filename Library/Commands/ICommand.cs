﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Commands
{
    public interface ICommand<in TEntity>
    {
        Task Execute(TEntity entity, LibraryContext context);
        IEnumerable<string> Validate(TEntity entity, LibraryContext context);
    }

    public interface ICommand<in TEntity, out TResult> : ICommand<TEntity>
    {
        TResult Result { get; }
    }
    
}
