using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using PropertyService.Dtos;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;
using PropertyService.Helpers;
using PropertyService.Services;
using Microsoft.Extensions.Logging;

namespace PropertyService.Models
{
    public class PropertiesModel : IPropertiesModel
    {
        private readonly ILogger<PropertiesModel> _logger;
        private readonly IMapper _mapper;
        private readonly IPropertiesRepository _propertiesRepository;
        private readonly IPropertiesService _productsAPI;

        public PropertiesModel(ILogger<PropertiesModel> logger, IMapper mapper, IPropertiesRepository propertiesRepository, IPropertiesService productsAPI)
        {
            _logger = logger;
            _mapper = mapper;
            _propertiesRepository = propertiesRepository;
            _productsAPI = productsAPI;
        }

        public async Task<Result<PropertyDto, ErrorResult>> AddProperty(PropertyDto property)
        {
            var productResult = await _propertiesRepository.AddProperty(property);
            if (productResult.IsFailure)
            {
                _logger.LogError(
                    "Failed to get product with id: {productName} from repository. {Error}",
                    property,
                    productResult.Error);
                return ResultGenerator.RepositoryError<PropertyDto>();
            }

            var product = productResult.Value;
            var productToReturn = _mapper.Map<PropertyDto>(product);

            return Result.Ok<PropertyDto, ErrorResult>(productToReturn);
        }

        public async Task<Result<PropertiesDto, ErrorResult>> getProperties()
        {
            var apidatapull = await _productsAPI.getProperties();
            if (apidatapull.IsFailure)
            {
                _logger.LogError(
                    "Failed to get products  from repository. {Error}",
                    apidatapull.Error);
                return ResultGenerator.RepositoryError<PropertiesDto>();
            }

            var productsToReturn = _mapper.Map<PropertiesDto>(apidatapull.Value);
            return Result.Ok<PropertiesDto, ErrorResult>(productsToReturn);
        }

        public async Task<Result<PropertiesDto, ErrorResult>> propertiesRepository()
        {
            var productTypeResult = await _propertiesRepository.propertiesRepository();
            if (productTypeResult.IsFailure)
            {
                _logger.LogError(
                    "Failed to get products  from repository. {Error}",
                    productTypeResult.Error);
                return ResultGenerator.RepositoryError<PropertiesDto>();
            }

            var productsToReturn = _mapper.Map<PropertiesDto>(productTypeResult.Value);

            return Result.Ok<PropertiesDto, ErrorResult>(productsToReturn);
        }
    }
}
