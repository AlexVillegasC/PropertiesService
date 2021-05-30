using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CSharpFunctionalExtensions;
using PropertyService.Configuration;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace PropertyService.Services
{
    public class PropertiesClient : IPropertiesClient
    {
        private const string Api = "/public/properties.json";

        private readonly RestClient _client;

        public PropertiesClient(IOptions<SystemBaseUrlsOptions> options)
        {
            _client = new RestClient(options.Value.APIName);
        }

        public IPropertiesClient AddJwtAuthenticator(string token)
        {
            _client.Authenticator = new JwtAuthenticator(token);
            return this;
        }

        public async Task<IRestResponse<Properties>> GetProperties()
        {
            var request = new RequestBuilder()
                .Get()
                .Resource(Api)
                .Build();

            var response = await _client.ExecuteTaskAsync<Properties>(request);

            return response;
        }
    }
}
