using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Contracts.Persistence;
using BookManager.Domain.Entities;
using BookManager.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Persistence.Authors
{
    public class AuthorRepository(ApplicationDbContext context) : GenericRepository<Author>(context),IAuthorRepository
    {
        public async Task<List<Author>> GetAllWithBookAsync()
        {
            return await context.Authors
                .Include(c => c.Books)
                .ToListAsync();
        }

        public async Task<Author> GetByIdWithBookAsync(Guid id)
        {
            return await context.Authors
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
