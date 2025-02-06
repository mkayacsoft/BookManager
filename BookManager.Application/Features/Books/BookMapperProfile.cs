using AutoMapper;
using BookManager.Application.Features.Books.Create;
using BookManager.Application.Features.Books.Dto;
using BookManager.Application.Features.Books.Update;
using BookManager.Domain.Entities;

namespace BookManager.Application.Features.Books
{
    public class BookMapperProfile:Profile
    {
        public BookMapperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
