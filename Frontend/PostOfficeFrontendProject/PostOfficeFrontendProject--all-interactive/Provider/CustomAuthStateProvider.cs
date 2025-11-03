namespace PostOfficeFrontendProject__all_interactive.Provider
{
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage; // Or update to newer storage if deprecated
    using System.Text;

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IConfiguration _config; // Inject config for key/issuer/audience
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ProtectedLocalStorage localStorage, IConfiguration config)
        {
            _localStorage = localStorage;
            _config = config;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var result = await _localStorage.GetAsync<string>("authToken");
                var token = result.Success ? result.Value : null;
                if (string.IsNullOrWhiteSpace(token))
                    return new AuthenticationState(_anonymous);

                var validatedClaims = ValidateAndParseClaimsFromJwt(token);
                if (validatedClaims == null)
                    return new AuthenticationState(_anonymous);

                var user = new ClaimsPrincipal(new ClaimsIdentity(validatedClaims, "jwt"));
                return new AuthenticationState(user);
            }
            catch
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetAsync("authToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            await _localStorage.DeleteAsync("authToken");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private IEnumerable<Claim> ValidateAndParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]); // From appsettings.json
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"], // e.g., "LoginApi"
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"], // e.g., "MainApi"
                ValidateLifetime = true, // Check expiry
                ClockSkew = TimeSpan.Zero // No tolerance for clock differences
            };

            try
            {
                handler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims;
            }
            catch
            {
                return null; // Invalid token
            }
        }
    }
}

