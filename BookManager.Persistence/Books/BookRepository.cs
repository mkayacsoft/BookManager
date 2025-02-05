using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Contracts.Persistence;
using BookManager.Domain.Entities;
using BookManager.Persistence.Shared;

namespace BookManager.Persistence.Books
{
    public class BookRepository(ApplicationDbContext context): GenericRepository<Book>(context), IBookRepository
    {
    }
}
