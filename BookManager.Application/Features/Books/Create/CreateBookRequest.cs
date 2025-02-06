using Microsoft.AspNetCore.Http;

namespace BookManager.Application.Features.Books.Create;

public record CreateBookRequest(
    string Title,
    string Description,
    Guid AuthorId,
    IFormFile? ImageData);
