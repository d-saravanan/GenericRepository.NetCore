using AutoMapper;
using GenericRepository.EntityFramework.SampleWebApi.Dtos;
using System;

namespace GenericRepository.EntityFramework.SampleWebApi.Controllers
{
    //TODO: Add more here to offload normal entity controllers...
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {
        internal readonly IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        internal Uri GetCountryLink<TId>(TId id)
        {
            return new Uri(Url.Link("DefaultHttpRoute", new { id = id }));
        }

        internal TDto DtoResult<TEntity, TDto>(TEntity entity)
        {
            return entity.ToDto<TEntity, TDto>(_mapper);
        }
        internal PaginatedDto<TDto> DtoResult<TEntity, TDto>(PaginatedList<TEntity> paginatedEntity)
            where TDto : IDto
        {
            return paginatedEntity.ToDto<TEntity, TDto>(_mapper);
        }
    }

    public static class DtoExtensions
    {
        public static TDto ToDto<TEntity, TDto>(this TEntity entity, IMapper mapper)
        {
            return mapper.Map<TEntity, TDto>(entity);
        }

        public static PaginatedDto<TDto> ToDto<TEntity, TDto>(this PaginatedList<TEntity> paginatedEntity, IMapper mapper)
            where TDto : IDto
        {
            return mapper.Map<PaginatedList<TEntity>, PaginatedDto<TDto>>(paginatedEntity);
        }
    }
}