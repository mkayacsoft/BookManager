using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookManager.Application.Contracts.Persistence;
using BookManager.Application.Features.Books.Create;
using BookManager.Application.Features.Books.Dto;
using BookManager.Application.Features.Books.Update;
using BookManager.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookManager.Application.Features.Books
{
    public class BookService(IBookRepository _bookRepository, IUnitOfWork _unitOfWork, IMapper _mapper,IWebHostEnvironment _webHostEnvironment) :IBookService
    {
        public async Task<ServiceResult<List<BookDto>>> GetAllAsync()
        {
            var result = await _bookRepository.GetAll();
            var bookAsDto = _mapper.Map<List<BookDto>>(result);

            return ServiceResult<List<BookDto>>.Success(bookAsDto);
        }

        public async  Task<ServiceResult<BookDto>> GetByIdAsync(Guid id)
        {
            var result = await _bookRepository.GetById(id);
            if (result == null)
            {
                return ServiceResult<BookDto>.Failure("Book not found", HttpStatusCode.NotFound);
            }

            var bookAsDto = _mapper.Map<BookDto>(result);

            return ServiceResult<BookDto>.Success(bookAsDto);

        }

        public async Task<ServiceResult<CreateBookResponse>> CreateAsync(CreateBookRequest createBookRequest)
        {
            var book = _mapper.Map<Book>(createBookRequest);
            if (createBookRequest.ImageData != null)
            {
                book.ImageData= await UploadImageAsync(createBookRequest.ImageData);
            }

            await _bookRepository.Create(book);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult<CreateBookResponse>.Success(new CreateBookResponse(book.Id));
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length ==0)
            {
                return string.Empty;
            }

            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"uploads/books");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/uploads/books/{fileName}";


        }

        public Task<ServiceResult> UpdateAsync(Guid id, UpdateBookRequest bookRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            var result = await _bookRepository.GetById(id);

            if (result is null)
            {
                return ServiceResult.Failure("Book not found.", HttpStatusCode.NotFound);
            }

            _bookRepository.Delete(result);
            _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);

        }
    }
}
