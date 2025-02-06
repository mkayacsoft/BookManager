using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Features.Authors.Create;
using BookManager.Application.Features.Authors.Dto;

namespace BookManager.Application.Features.Authors
{
    public interface IAuthorService
    {
        Task<ServiceResult<List<AuthorDto>>> GetAllAsync();
        Task<ServiceResult<AuthorDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<CreateAuthorResponse>> CreateAsync(CreateAuthorRequest author);
        Task<ServiceResult> UpdateAsync(AuthorDto author);
        Task<ServiceResult> DeleteAsync(Guid id);



    }
}
