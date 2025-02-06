using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Application.Features.Authors.Update;
using FluentValidation;

namespace BookManager.Application.Features.Authors.Validators
{
    public class UpdateAuthorValidator:AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.FirstName)
               
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters.")
                .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                .MaximumLength(20).WithMessage("Last name cannot exceed 50 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("Last name can only contain letters.")
                .When(x => !string.IsNullOrEmpty(x.LastName));


            RuleFor(x => x.Genre)
                .MaximumLength(30).WithMessage("Genre cannot exceed 30 characters.")
                .When(x => !string.IsNullOrEmpty(x.Genre));

        }
    }
}
