using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace PropertyService.Services
{
    public class PropertiesService : IPropertiesService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PropertiesService> _logger;
        private readonly IPropertiesApi _api;

        public PropertiesService(IMapper mapper, ILogger<PropertiesService> logger, IPropertiesApi serviceApi)
        {
            _mapper = mapper;
            _logger = logger;
            _api = serviceApi;
        }

        public async Task<Result<Properties>> getProperties()
        {
            return await _api.getProperties();
        }
    }
}
