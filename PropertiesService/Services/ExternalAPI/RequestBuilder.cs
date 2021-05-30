using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PropertyService.Dtos;
using PropertyService.Entities;
using PropertyService.FunctionalExtensions;
using RestSharp;

namespace PropertyService.Services
{
    public class RequestBuilder
    {
        private const string Bearer = "Bearer";
        private const string Authorization = "Authorization";
        private readonly RestRequest _restRequest;

        public RequestBuilder()
        {
            _restRequest = new RestRequest();
        }

        public RequestBuilder Get()
        {
            _restRequest.Method = Method.GET;
            return this;
        }

        public RequestBuilder Post()
        {
            _restRequest.Method = Method.POST;
            return this;
        }

        public RequestBuilder Put()
        {
            _restRequest.Method = Method.PUT;
            return this;
        }

        public RequestBuilder Resource(string resource)
        {
            _restRequest.Resource = resource;
            return this;
        }

        public RequestBuilder AddJsonBody(object obj)
        {
            _restRequest.AddHeader("Content-Type", "application/json");
            _restRequest.AddJsonBody(obj);
            return this;
        }

        public RequestBuilder AddQueryParameter(string name, string value)
        {
            _restRequest.AddQueryParameter(name, value);
            return this;
        }

        public RequestBuilder AddAuthorization(string token)
        {
            _restRequest.AddHeader(Authorization, $"{Bearer} {token}");
            return this;
        }

        public RestRequest Build()
        {
            return _restRequest;
        }
    }
}
