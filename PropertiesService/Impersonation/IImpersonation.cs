using System;
using System.Threading.Tasks;

namespace PropertyService.Impersonation
{
    public interface IImpersonation
    {
        void Run(Action action);

        Task<T> Run<T>(Func<T> func);

        Task<T> Run<T>(Func<T> func, LogonType logonType, LogonProvider logonProvider);
    }
}