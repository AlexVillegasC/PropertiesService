using System;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Configuration;
using Microsoft.Extensions.Options;
using Polly;
using RestSharp;

namespace PropertyService.Services
{
    public abstract class ClientApiBase
    {
        private readonly RetryOptions _retry;
        private readonly Func<int, TimeSpan> _sleepDurationProvider;

        protected ClientApiBase(IOptions<RetryOptions> retry, Func<int, TimeSpan> sleepDurationProvider = null)
        {
            _retry = retry.Value;
            _sleepDurationProvider = sleepDurationProvider ?? (i => TimeSpan.FromSeconds(_retry.DelayInSeconds));
        }

        protected async Task<Result> WaitAndRetryAsync(Func<Task<IRestResponse>> action, Action<DelegateResult<IRestResponse>, TimeSpan> onRetry, string failureOutcomeErrorMessage)
        {
            var policyResult = await Policy
                .HandleResult<IRestResponse>(r => IsRetryable(r.StatusCode))
                .WaitAndRetryAsync(_retry.NumberOfRetries, _sleepDurationProvider, onRetry)
                .ExecuteAndCaptureAsync(action);

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                var error = failureOutcomeErrorMessage;
                return Result.Fail(error);
            }

            var result = policyResult.Result;

            if (!result.IsSuccessful)
            {
                return Result.Fail($"{result.StatusCode}: {result.Content}");
            }

            return Result.Ok();
        }

        protected async Task<Result<TOutput>> WaitAndRetryAsync<TInput, TOutput>(Func<Task<IRestResponse<TInput>>> action, Func<TInput, TOutput> onSuccess, Action<DelegateResult<IRestResponse<TInput>>, TimeSpan> onRetry, string failureOutcomeErrorMessage)
        {
            var policyResult = await Policy
                .HandleResult<IRestResponse<TInput>>(r => IsRetryable(r.StatusCode))
                .WaitAndRetryAsync(_retry.NumberOfRetries, _sleepDurationProvider, onRetry)
                .ExecuteAndCaptureAsync(action);

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                var error = failureOutcomeErrorMessage;
                return Result.Fail<TOutput>(error);
            }

            var result = policyResult.Result;

            if (!result.IsSuccessful)
            {
                return Result.Fail<TOutput>($"{result.StatusCode}: {result.Content}");
            }

            var success = onSuccess(result.Data);

            return Result.Ok(success);
        }

        protected async Task<Result<TOutput>> WaitAndRetryAsync<TInput, TOutput>(Func<Task<IRestResponse<TInput>>> action, Func<TInput, Result<TOutput>> onSuccess, Action<DelegateResult<IRestResponse<TInput>>, TimeSpan> onRetry, string failureOutcomeErrorMessage)
        {
            var policyResult = await Policy
                .HandleResult<IRestResponse<TInput>>(r => IsRetryable(r.StatusCode))
                .WaitAndRetryAsync(_retry.NumberOfRetries, _sleepDurationProvider, onRetry)
                .ExecuteAndCaptureAsync(action);

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                var error = failureOutcomeErrorMessage;
                return Result.Fail<TOutput>(error);
            }

            var result = policyResult.Result;

            if (!result.IsSuccessful)
            {
                return Result.Fail<TOutput>($"{result.StatusCode}: {result.Content}");
            }

            return onSuccess(result.Data);
        }

        private bool IsRetryable(HttpStatusCode httpStatusCode)
        {
            return httpStatusCode == HttpStatusCode.ServiceUnavailable ||
                   httpStatusCode == HttpStatusCode.TooManyRequests ||
                   httpStatusCode == HttpStatusCode.NotFound;
        }
    }
}
