using BookManager.Application.Features.Books.Dto;

namespace BookManager.Application.Features.Authors.Dto;

public record AuthorDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Genre,
    ICollection<BookDto> Books,
    DateTime CreatedAt,
    DateTime UpdatedAt);
 