using AutoMapper;
using GenericRepository.EntityFramework.SampleCore.Entities;
using GenericRepository.EntityFramework.SampleWebApi.Dtos;
using GenericRepository.EntityFramework.SampleWebApi.Mapping;

namespace GenericRepository.EntityFramework.SampleWebApi.App_Start
{
    public static class MapperConfig
    {
        private static IMapper _mapper;
        public static IMapper RegisterMappings()
        {
            AutoMapperConfig.Configure();
            AutoMapperConfig.InitProfiles<Country, CountryDto>();
            _mapper = new Mapper(Mapper.Configuration);
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
            return _mapper;
        }

    }
}