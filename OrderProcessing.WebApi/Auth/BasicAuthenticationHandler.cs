using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace OrderProcessing.WebApi.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization header"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization header does not start with 'Basic'"));
            }

            var authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)));
            var authSplitted = authBase64Decoded.Split(new[] { ':' }, 2);

            if (authSplitted.Length != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header format"));
            }

            var userName = authSplitted[0];
            var userPassword = authSplitted[1];

            if (userName != "vs" || userPassword != "rekrutacja")
            {
                return Task.FromResult(AuthenticateResult.Fail(string.Format("The password is incorrect for the user '{0}'", userName)));
            }

            var client = new BasicAuthenticationClient
            {
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = userName
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
                new Claim(ClaimTypes.Name, userName)
            }));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}