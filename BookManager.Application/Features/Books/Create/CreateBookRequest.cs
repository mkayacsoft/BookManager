using Microsoft.AspNetCore.Http;

namespace BookManager.Application.Features.Books.Create;

public record CreateBookRequest(string Genre,
    string Title,
    string Description,
    IFormFile? ImageData);
