using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Application.Features.Authors.Create;

public record CreateAuthorRequest(
    string FirstName,
    string LastName,
    string Genre);

