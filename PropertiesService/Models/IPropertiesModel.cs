using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Dtos;
using PropertyService.FunctionalExtensions;

namespace PropertyService.Models
{
    public interface IPropertiesModel
    {
        Task<Result<PropertyDto, ErrorResult>> AddProperty(PropertyDto property);

        public Task<Result<PropertiesDto, ErrorResult>> getProperties();

        public Task<Result<PropertiesDto, ErrorResult>> propertiesRepository();
        
    }
}
