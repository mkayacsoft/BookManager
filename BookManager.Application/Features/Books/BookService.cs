using AutoMapper;
using BookManager.Application.Contracts.Persistence;
using BookManager.Application.Features.Books.Create;
using BookManager.Application.Features.Books.Dto;
using BookManager.Application.Features.Books.Update;
using BookManager.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BookManager.Application.Features.Books
{
    public class BookService(IBookRepository _bookRepository, IUnitOfWork _unitOfWork, IMapper _mapper,IWebHostEnvironment _webHostEnvironment,IRedisService _redisService) :IBookService
    {
        public async Task<ServiceResult<List<BookDto>>> GetAllAsync()
        {
            const string cacheKey = "BookList";

            var cacheData = await _redisService.GetFromCacheAsync<List<BookDto>>(cacheKey);
            if (cacheData != null)
            {
                return ServiceResult<List<BookDto>>.Success(cacheData);
            }

            var result = await _bookRepository.GetAll();
            var bookAsDto = _mapper.Map<List<BookDto>>(result);

            await _redisService.SetCacheAsync(cacheKey, bookAsDto,TimeSpan.FromMinutes(30));

            return ServiceResult<List<BookDto>>.Success(bookAsDto);
        }

        public async  Task<ServiceResult<BookDto>> GetByIdAsync(Guid id)
        {

            string cacheKey = $"Book-{id}";
            var cacheData = await _redisService.GetFromCacheAsync<BookDto>(cacheKey);

            if (cacheData != null)
            {
                return ServiceResult<BookDto>.Success(cacheData);
            }

            var result = await _bookRepository.GetById(id);
            if (result == null)
            {
                return ServiceResult<BookDto>.Failure("Book not found", HttpStatusCode.NotFound);
            }

            var bookAsDto = _mapper.Map<BookDto>(result);

            await _redisService.SetCacheAsync(cacheKey, bookAsDto, TimeSpan.FromMinutes(30));

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

            string cacheKey = $"Book-{book.Id}";

            var bookAAsDto = _mapper.Map<BookDto>(book);

            await _redisService.SetCacheAsync(cacheKey, bookAAsDto, TimeSpan.FromMinutes(30));

            string allBooksCacheKey = "BookList";
            
            var cachedBooks = await _redisService.GetFromCacheAsync<List<BookDto>>(allBooksCacheKey);

            if (cachedBooks != null)
            {
                cachedBooks.Add(bookAAsDto);
                await _redisService.SetCacheAsync(allBooksCacheKey, cachedBooks, TimeSpan.FromMinutes(30));
            }
            else
            {
                await _redisService.SetCacheAsync(allBooksCacheKey, new List<BookDto> { bookAAsDto }, TimeSpan.FromMinutes(30));
            }

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

        public async Task<ServiceResult> UpdateAsync(Guid id, UpdateBookRequest bookRequest)
        {
            var book = await _bookRepository.GetById(id);

            if (book is null)
            {
                return ServiceResult.Failure("Book not found",HttpStatusCode.NotFound);
            }
            _mapper.Map(bookRequest, book);
            _bookRepository.Update(book);
            await _unitOfWork.SaveChangeAsync();

            string bookCachekEY = $"book_{book.Id}";
            var bookAsDto = _mapper.Map<BookDto>(book);
            await _redisService.SetCacheAsync(bookCachekEY, bookAsDto, TimeSpan.FromMinutes(10));

            string allBooksCacheKey = "BookList";
            var cachedBooks = await _redisService.GetFromCacheAsync<List<BookDto>>(allBooksCacheKey);

            if (cachedBooks != null)
            {
                var index = cachedBooks.FindIndex(c => c.Id == book.Id);
                if (index != -1)
                {
                    cachedBooks[index] = bookAsDto; // Güncellenmiş veriyi yerine koy
                    await _redisService.SetCacheAsync(allBooksCacheKey, cachedBooks, TimeSpan.FromMinutes(10));
                }
            }

            return ServiceResult.Success(HttpStatusCode.NoContent);
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

            string bookCacheKey = $"book_{result.Id}";
            await _redisService.RemoveCacheAsync(bookCacheKey);

            string allBooksCacheKey = "BookList";
            var cachedBooks = await _redisService.GetFromCacheAsync<List<BookDto>>(allBooksCacheKey);

            if (cachedBooks != null)
            {
                cachedBooks.RemoveAll(c => c.Id == result.Id); // Silinen şirketi listeden kaldır
                await _redisService.SetCacheAsync(allBooksCacheKey, cachedBooks, TimeSpan.FromMinutes(10));
            }

            return ServiceResult.Success(HttpStatusCode.NoContent);

        }
    }
}
