using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Dtos;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;

namespace PropertyService.Services
{
    public interface IPropertiesService
    {
        Task<Result<Entities.Properties>> getProperties();
    }
}
