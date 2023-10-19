using Budget.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Budget.API.Handler
{
    public class EfAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly BudgetDbContext _database;

        public EfAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            BudgetDbContext database)
            : base(options, logger, encoder, clock) => _database = database;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Заголовок Authorization пропущен");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var data = authHeader.Parameter;
            if (data == null)
                return AuthenticateResult.Fail("Пользователь не найден");

            string login = data.Substring(0, data.IndexOf(':'));
            string password = data.Substring(data.IndexOf(':'));

            var user = await _database.Users.FirstOrDefaultAsync(p => p.Name.Equals(login) && p.Password.Equals(password)).ConfigureAwait(false);

            if (user == null) 
                return AuthenticateResult.Fail("Пользователь не найден");

            var claims = new[] { new Claim(ClaimTypes.Name, user.Name) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
