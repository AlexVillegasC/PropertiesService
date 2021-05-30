using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Configuration;
using PropertyService.Entities;
using PropertyService.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PropertyService.Services
{
    public class PropertiesApi : ClientApiBase, IPropertiesApi
    {
        private readonly IHttpContextTokenFetcher _tokenFetcher;
        private readonly ILogger<PropertiesApi> _logger;
        private readonly IPropertiesClient _client;
        private readonly RetryOptions _retry;

        public PropertiesApi(
            IHttpContextTokenFetcher httpTokenFetcherAccessor,
            ILogger<PropertiesApi> logger,
            IPropertiesClient client,
            IOptions<RetryOptions> retry,
            Func<int, TimeSpan> sleepDurationProvider = null)
            : base(retry, sleepDurationProvider)
        {
            _tokenFetcher = httpTokenFetcherAccessor;
            _logger = logger;
            _retry = retry.Value;
            _client = client;
        }

        public async Task<Result<Properties>> getProperties()
        {
            // return await _client.GetProducts();
            return await WaitAndRetryAsync(
            async () => await _client.GetProperties(),
            responseDto => responseDto,
            (result, span) => _logger.LogError($"{result.Result.StatusCode}: {result.Result.Content}"),
            $"Failed to get units data from vortex material-service {_retry.NumberOfRetries} times");
        }

        private Task<Result<IPropertiesClient>> GetClient()
        {
            return Task.FromResult(Result.Ok(_client));

            // TODO:[Team]<-[Golan] - uncomment when authentication is enabled
            // return _tokenFetcher.GetToken()
            //    .OnSuccess(token => _client.AddJwtAuthenticator(token));
        }
    }
}
