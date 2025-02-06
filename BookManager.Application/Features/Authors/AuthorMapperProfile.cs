using AutoMapper;
using BookManager.Application.Features.Authors.Dto;
using BookManager.Domain.Entities;

namespace BookManager.Application.Features.Authors
{
    public class AuthorMapperProfile:Profile
    {
        public AuthorMapperProfile()
        {
            CreateMap<Author,AuthorDto>().ReverseMap();
        }
    }
}
