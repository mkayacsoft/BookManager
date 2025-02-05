using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Application.Features.Authors.Update;

public record UpdateAuthorRequest(string FirstName,
    string LastName,
    string Genre);
 