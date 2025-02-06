using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Application.Features.Books.Update;

public record UpdateBookRequest(
    string? Title,
    string? Description,
    IFormFile? ImageData);
