using AutoMapper;
using GenericRepository.EntityFramework.SampleCore.Entities;
using GenericRepository.EntityFramework.SampleWebApi.Dtos;
using GenericRepository.EntityFramework.SampleWebApi.RequestModels;
using GenericService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace GenericRepository.EntityFramework.SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : BaseController
    {
        private readonly GenericService<Country, int> _countryService;
        public CountriesController(GenericService<Country, int> countryService, IMapper mapper) : base(mapper)
        {
            _countryService = countryService;
        }

        // GET api/countries?pageindex=1&pagesize=5
        [HttpGet]
        public async Task<PaginatedDto<CountryDto>> GetCountries(int pageIndex, int pageSize)
        {
            PaginatedList<Country> countries = await _countryService.SearchAsync(new CountrySearchCondition
            {
                PageNo = pageIndex,
                RecordsPerPage = pageSize
            });
            PaginatedDto<CountryDto> countryPaginatedDto = _mapper.Map<PaginatedList<Country>, PaginatedDto<CountryDto>>(countries);
            return countryPaginatedDto;
            //return DtoResult<Country, CountryDto>(countries);
        }

        // GET api/countries/1
        public async Task<HttpResponseMessage> GetCountry(int id)
        {
            Country country = await _countryService.GetByIdAsync(id);
            if (country == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var responseContent = new ObjectContent<CountryDto>(DtoResult<Country, CountryDto>(country), new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = responseContent
            };
        }

        // POST api/countries
        public async Task<HttpResponseMessage> PostCountry(CountryRequestModel requestModel)
        {
            Country country = _mapper.Map<CountryRequestModel, Country>(requestModel);
            country.CreatedOn = DateTimeOffset.Now;

            await _countryService.AddAsync(country);

            CountryDto countryDto = _mapper.Map<Country, CountryDto>(country);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new ObjectContent<CountryDto>(countryDto, new JsonMediaTypeFormatter())
            };
            response.Headers.Location = GetCountryLink(country.Id);

            return response;
        }

        // PUT api/countries/1
        public async Task<HttpResponseMessage> PutCountry(int id, CountryRequestModel requestModel)
        {
            Country country = await _countryService.GetByIdAsync(id);
            if (country == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }


            Country updatedCountry = requestModel.ToCountry(country);
            await _countryService.UpdateAsync(country);

            CountryDto countryDto = _mapper.Map<Country, CountryDto>(country);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<CountryDto>(countryDto, new JsonMediaTypeFormatter())
            };
        }

        // DELETE api/countries/1
        public async Task<HttpResponseMessage> DeleteCountry(int id)
        {
            await _countryService.DeleteAsync(id);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}