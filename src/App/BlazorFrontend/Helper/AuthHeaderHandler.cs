using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace PostOfficeFrontendProject__all_interactive.Handler
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ProtectedLocalStorage _localStorage;

        public AuthHeaderHandler(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _localStorage.GetAsync<string>("authToken");
                var token = result.Success ? result.Value : null;

                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch
            {
                // اگر خطایی پیش اومد، فقط درخواست بدون توکن بره
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
