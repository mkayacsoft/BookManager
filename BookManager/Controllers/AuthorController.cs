using BookManager.Application.Features.Authors;
using BookManager.Application.Features.Authors.Create;
using BookManager.Application.Features.Authors.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    public class AuthorController(IAuthorService _authorService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var serviceResult = await _authorService.GetAllAsync();
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var serviceResult = await _authorService.GetByIdAsync(id);
            return CreateActionResult(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorRequest createAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _authorService.CreateAsync(createAuthor);
            return CreateActionResult(serviceResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromForm] UpdateAuthorRequest updateAuthorRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var serviceResult = await _authorService.UpdateAsync(id,updateAuthorRequest);
            return CreateActionResult(serviceResult);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var serviceResult = await _authorService.DeleteAsync(id);
            return CreateActionResult(serviceResult);
        }
    }
}
