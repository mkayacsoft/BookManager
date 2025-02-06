using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookManager.Application.Contracts.Persistence;
using BookManager.Application.Features.Authors.Create;
using BookManager.Application.Features.Authors.Dto;
using BookManager.Application.Features.Authors.Update;
using BookManager.Domain.Entities;

namespace BookManager.Application.Features.Authors
{
    public class AuthorService(IAuthorRepository _authorRepository,IUnitOfWork _unitOfWork,IMapper _mapper): IAuthorService
    {
        public async Task<ServiceResult<List<AuthorDto>>> GetAllAsync()
        {
            var result = await _authorRepository.GetAllWithBookAsync();
            var authorAsDto = _mapper.Map<List<AuthorDto>>(result);

            return ServiceResult<List<AuthorDto>>.Success(authorAsDto);
        }

        public async Task<ServiceResult<AuthorDto>> GetByIdAsync(Guid id)
        {
            var result = await _authorRepository.GetByIdWithBookAsync(id);
            if (result == null)
            {
                return ServiceResult<AuthorDto>.Failure(" Author not found",HttpStatusCode.NotFound);
            }

            var authorAsDto = _mapper.Map<AuthorDto>(result);
            return ServiceResult<AuthorDto>.Success(authorAsDto);

        }

        public async Task<ServiceResult<CreateAuthorResponse>> CreateAsync(CreateAuthorRequest createAuthorRequest)
        {
            var author = _mapper.Map<Author>(createAuthorRequest);

            await _authorRepository.Create(author);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult<CreateAuthorResponse>.Success(new CreateAuthorResponse(author.Id));
        }

        public async Task<ServiceResult> UpdateAsync(Guid authorId ,UpdateAuthorRequest authorRequest)
        {
            var author = await _authorRepository.GetById(authorId);
            if (author is null)
            {
                return ServiceResult.Failure("Author not found",HttpStatusCode.NotFound);
            }

            _mapper.Map(authorRequest, author);

             _authorRepository.Update(author);
            await _unitOfWork.SaveChangeAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            var author = await _authorRepository.GetById(id);

            if (author is null)
            {
                return ServiceResult.Failure("Author not found", HttpStatusCode.NotFound);
            }

            _authorRepository.Delete(author);
            await _unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
