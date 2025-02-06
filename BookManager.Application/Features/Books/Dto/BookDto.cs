using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Application.Features.Books.Dto;

public record BookDto(
    Guid Id,
    string Title, 
    string Description,
    string ImageData, 
    DateTime CreatedAt,
    DateTime UpdatedAt);

