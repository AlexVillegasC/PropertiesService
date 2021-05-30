using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyService.Configuration
{
    public class SqlOptions
    {
        public string ConnectionString { get; set; }
    }
}
