using BookManager.Application.Features.Authors.Create;
using BookManager.Application.Features.Authors.Dto;
using BookManager.Application.Features.Authors.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Features.Books.Create;
using BookManager.Application.Features.Books.Dto;
using BookManager.Application.Features.Books.Update;
using Microsoft.AspNetCore.Http;

namespace BookManager.Application.Features.Books
{
    public interface IBookService
    {
        Task<ServiceResult<List<BookDto>>> GetAllAsync();
        Task<ServiceResult<BookDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<CreateBookResponse>> CreateAsync(CreateBookRequest createBookRequest);
        Task<string> UploadImageAsync(IFormFile imageFile);
        Task<ServiceResult> UpdateAsync(Guid id, UpdateBookRequest bookRequest);
        Task<ServiceResult> DeleteAsync(Guid id);
    }
}
