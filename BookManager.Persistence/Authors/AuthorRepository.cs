using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Contracts.Persistence;
using BookManager.Domain.Entities;
using BookManager.Persistence.Shared;

namespace BookManager.Persistence.Authors
{
    public class AuthorRepository(ApplicationDbContext context) : GenericRepository<Author>(context),IAuthorRepository
    {
    }
}
