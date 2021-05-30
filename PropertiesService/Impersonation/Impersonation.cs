using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Win32.SafeHandles;

namespace PropertyService.Impersonation
{
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1310:ElementShouldNotContainUnderscore", Justification = "Is a Constant, not required for the code analysis")]
    public class Impersonation : IImpersonation
    {
        private const int LOGON32PROVIDERDEFAULT = 0;

        private const int LOGON32LOGONNEWCREDENTIALS = 9;

        private ImpersonationOptions _options;

        public Impersonation(IOptions<ImpersonationOptions> options)
        {
            _options = options.Value;
        }

        public Impersonation(ImpersonationOptions options)
        {
            _options = options;
        }

        public void Run(Action action)
        {
            WindowsIdentity.RunImpersonated(GetToken(), action);
        }

        public Task<T> Run<T>(Func<T> func)
        {
            return Task.FromResult(WindowsIdentity.RunImpersonated(GetToken(), func));
        }

        public Task<T> Run<T>(Func<T> func, LogonType logonType, LogonProvider logonProvider)
        {
            return Task.FromResult(WindowsIdentity.RunImpersonated(GetToken(logonType, logonProvider), func));
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool LogonUser(string username, string domain, string password, int logonType, int logonProvider, out SafeAccessTokenHandle token);

        private SafeAccessTokenHandle GetToken()
        {
            var safeAccessTokenHandle = LogonUser(_options.UserName, _options.Domain, _options.Password);

            if (safeAccessTokenHandle.IsInvalid)
            {
                var win32Error = Marshal.GetLastWin32Error();

                throw new Win32Exception($"The user name or password is incorrect(ERROR_LOGON_FAILURE {win32Error}).");
            }

            return safeAccessTokenHandle;
        }

        private SafeAccessTokenHandle LogonUser(string username, string domain, string password)
        {
            LogonUser(username, domain, password, LOGON32LOGONNEWCREDENTIALS, LOGON32PROVIDERDEFAULT, out var tokenHandle);

            return tokenHandle;
        }

        private SafeAccessTokenHandle GetToken(LogonType logonType, LogonProvider logonProvider)
        {
            var safeAccessTokenHandle = LogonUser(_options.UserName, _options.Domain, _options.Password, logonType, logonProvider);

            if (safeAccessTokenHandle.IsInvalid)
            {
                var win32Error = Marshal.GetLastWin32Error();

                throw new Win32Exception($"The user name or password is incorrect(ERROR_LOGON_FAILURE {win32Error}).");
            }

            return safeAccessTokenHandle;
        }

        private SafeAccessTokenHandle LogonUser(string username, string domain, string password, LogonType logonType, LogonProvider logonProvider)
        {
            LogonUser(username, domain, password, (int)logonType, (int)logonProvider, out var tokenHandle);

            return tokenHandle;
        }
    }
}
