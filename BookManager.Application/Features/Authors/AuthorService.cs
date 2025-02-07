using AutoMapper;
using BookManager.Application.Contracts.Persistence;
using BookManager.Application.Features.Authors.Create;
using BookManager.Application.Features.Authors.Dto;
using BookManager.Application.Features.Authors.Update;
using BookManager.Domain.Entities;
using System.Net;

namespace BookManager.Application.Features.Authors
{
    public class AuthorService(IAuthorRepository _authorRepository,IUnitOfWork _unitOfWork,IMapper _mapper,IRedisService _redisService): IAuthorService
    {
        public async Task<ServiceResult<List<AuthorDto>>> GetAllAsync()
        {
            const string cacheKey = "AuthorList";
            var cacheData = await _redisService.GetFromCacheAsync<List<AuthorDto>>(cacheKey);
            if (cacheData != null)
            {
                return ServiceResult<List<AuthorDto>>.Success(cacheData);
            }

            var result = await _authorRepository.GetAllWithBookAsync();
            var authorAsDto = _mapper.Map<List<AuthorDto>>(result);

            await _redisService.SetCacheAsync(cacheKey, authorAsDto, TimeSpan.FromMinutes(30));

            return ServiceResult<List<AuthorDto>>.Success(authorAsDto);
        }

        public async Task<ServiceResult<AuthorDto>> GetByIdAsync(Guid id)
        {
            string cacheKey = $"Author-{id}";
            var cacheData = await _redisService.GetFromCacheAsync<AuthorDto>(cacheKey);

            if (cacheData != null)
            {
                return ServiceResult<AuthorDto>.Success(cacheData);
            }

            var result = await _authorRepository.GetByIdWithBookAsync(id);
            if (result == null)
            {
                return ServiceResult<AuthorDto>.Failure(" Author not found",HttpStatusCode.NotFound);
            }
            var authorAsDto = _mapper.Map<AuthorDto>(result);

            await _redisService.SetCacheAsync(cacheKey, authorAsDto, TimeSpan.FromMinutes(30));

            return ServiceResult<AuthorDto>.Success(authorAsDto);

        }

        public async Task<ServiceResult<CreateAuthorResponse>> CreateAsync(CreateAuthorRequest createAuthorRequest)
        {
            var author = _mapper.Map<Author>(createAuthorRequest);

            await _authorRepository.Create(author);
            await _unitOfWork.SaveChangeAsync();

            string cacheKey = $"Author-{author.Id}";

            var authorAsDto = _mapper.Map<AuthorDto>(author);

            await _redisService.SetCacheAsync(cacheKey, authorAsDto, TimeSpan.FromMinutes(30));

            string allAuthorsCacheKey = "AuthorList";

            var cachedAuthors = await _redisService.GetFromCacheAsync<List<AuthorDto>>(allAuthorsCacheKey);

            if (cachedAuthors != null)
            {
                cachedAuthors.Add(authorAsDto);
                await _redisService.SetCacheAsync(allAuthorsCacheKey, cachedAuthors, TimeSpan.FromMinutes(30));
            }
            else
            {
                await _redisService.SetCacheAsync(allAuthorsCacheKey, new List<AuthorDto> { authorAsDto }, TimeSpan.FromMinutes(30));
            }

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

            string cacheKey = $"Author-{author.Id}";

            var authorAsDto = _mapper.Map<AuthorDto>(author);

            await _redisService.SetCacheAsync(cacheKey, authorAsDto, TimeSpan.FromMinutes(30));

            string allAuthorsCacheKey = "AuthorList";

            var cachedAuthors = await _redisService.GetFromCacheAsync<List<AuthorDto>>(allAuthorsCacheKey);

            if (cachedAuthors != null)
            {
                var authorIndex = cachedAuthors.FindIndex(a => a.Id == author.Id);
                if (authorIndex != -1)
                {
                    cachedAuthors[authorIndex] = authorAsDto;
                    await _redisService.SetCacheAsync(allAuthorsCacheKey, cachedAuthors, TimeSpan.FromMinutes(30));
                }
            }
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

            string cacheKey = $"Author-{author.Id}";
            await _redisService.RemoveCacheAsync(cacheKey);

            string allAuthorsCacheKey = "AuthorList";

            var cachedAuthors = await _redisService.GetFromCacheAsync<List<AuthorDto>>(allAuthorsCacheKey);

            if (cachedAuthors != null)
            {
                var authorIndex = cachedAuthors.FindIndex(a => a.Id == author.Id);
                if (authorIndex != -1)
                {
                    cachedAuthors.RemoveAt(authorIndex);
                    await _redisService.SetCacheAsync(allAuthorsCacheKey, cachedAuthors, TimeSpan.FromMinutes(30));
                }
            }

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
