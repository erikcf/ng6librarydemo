﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Domain.Models;

namespace Library.Services
{
    public interface IBookService
    {
        Task<IList<Book>> GetAllBooks(string name);
        Task<Book> GetBookById(int id);
    }
}
