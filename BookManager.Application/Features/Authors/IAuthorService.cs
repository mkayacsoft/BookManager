using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Features.Authors.Create;
using BookManager.Application.Features.Authors.Dto;
using BookManager.Application.Features.Authors.Update;

namespace BookManager.Application.Features.Authors
{
    public interface IAuthorService
    {
        Task<ServiceResult<List<AuthorDto>>> GetAllAsync();
        Task<ServiceResult<AuthorDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<CreateAuthorResponse>> CreateAsync(CreateAuthorRequest author);
        Task<ServiceResult> UpdateAsync(Guid id,UpdateAuthorRequest authorRequest);
        Task<ServiceResult> DeleteAsync(Guid id);



    }
}
