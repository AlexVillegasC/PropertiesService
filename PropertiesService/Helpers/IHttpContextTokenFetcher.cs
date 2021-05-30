using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace PropertyService.Helpers
{
    public interface IHttpContextTokenFetcher
    {
        Task<Result<string>> GetToken();
    }
}