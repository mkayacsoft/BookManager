using BookManager.Application.Features.Books;
using BookManager.Application.Features.Books.Create;
using BookManager.Application.Features.Books.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    public class BookController(IBookService _bookService) : CustomBaseController
    {
        
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllAsync();
            return CreateActionResult(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return CreateActionResult(book);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] CreateBookRequest request)
        {
            var response = await _bookService.CreateAsync(request);
            return CreateActionResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBook(Guid id,[FromForm] UpdateBookRequest request)
        {
            var response = await _bookService.UpdateAsync(id,request);
            return CreateActionResult(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var response = await _bookService.DeleteAsync(id);
            return CreateActionResult(response);
        }
    }
}
