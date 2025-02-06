using AutoMapper;
using BookManager.Application.Features.Authors.Create;
using BookManager.Application.Features.Authors.Dto;
using BookManager.Application.Features.Authors.Update;
using BookManager.Domain.Entities;

namespace BookManager.Application.Features.Authors
{
    public class AuthorMapperProfile:Profile
    {
        public AuthorMapperProfile()
        {
            CreateMap<Author,AuthorDto>().ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books)).ReverseMap();
            CreateMap<CreateAuthorRequest, Author>();
            CreateMap<UpdateAuthorRequest, Author>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
