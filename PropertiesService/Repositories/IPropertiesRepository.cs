using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Dtos;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;

namespace PropertyService.Services
{
    public interface IPropertiesRepository
    {

        Task<Result<Properties, ErrorResult>> propertiesRepository();

        Task<Result<Property, ErrorResult>> AddProperty(PropertyDto property);

    }
}
