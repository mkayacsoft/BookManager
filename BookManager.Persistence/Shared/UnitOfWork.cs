using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Contracts.Persistence;

namespace BookManager.Persistence.Shared
{
    public class UnitOfWork(ApplicationDbContext context): IUnitOfWork
    {
        public Task<int> SaveChangeAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
