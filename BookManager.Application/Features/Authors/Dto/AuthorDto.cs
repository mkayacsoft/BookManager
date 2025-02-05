using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Features.Books.Dto;
using BookManager.Domain.Entities;

namespace BookManager.Application.Features.Authors.Dto;

public record AuthorDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Genre,
    BookDto Book,
    DateTime CreatedAt,
    DateTime UpdatedAt);
 