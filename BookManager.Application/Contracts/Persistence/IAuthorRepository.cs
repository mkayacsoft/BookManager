using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Domain.Entities;

namespace BookManager.Application.Contracts.Persistence
{
    public interface IAuthorRepository: IGenericRepository<Author>
    { 
        Task<List<Author>> GetAllWithBookAsync();
       

        Task<Author> GetByIdWithBookAsync(Guid id);

    }
}
