using Budget.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Budget.API.Handler
{
    public class EfAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public EfAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            BudgetDbContext database)
            : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Заголовок Authorization пропущен");

            throw new NotImplementedException();
        }
    }
}
