using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;
using RestSharp;

namespace PropertyService.Services
{
    public interface IPropertiesClient : IClient<IPropertiesClient>
    {
        Task<IRestResponse<Properties>> GetProperties();
    }
}
