using BookManager.Application.Features.Books.Update;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookManager.Application.Features.Books.Validators
{
    public class UpdateBookValidator:AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookValidator()
        {
           

            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.ImageData)
                .Must(HaveAllowedExtension)
                .WithMessage("Invalid file type. Allowed extensions are: .jpg,.jpeg,.png,.gif,.bmp,.tiff ,.tif,.svg,.webp,.ico");

        }
        private bool HaveAllowedExtension(IFormFile? file)
        {
            if (file == null) return true; // Dosya yoksa kontrol etme

            var allowedExtensions = new[]
                { ".jpg", ".jpeg", ".png", ".gif", ".bmp ", ".tiff", ".tif", ".svg ", ".webp ", ".ico" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
