using Budget.API.Handler;
using Budget.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;

namespace Budget.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions(p => p.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication("EfAuth").AddScheme<AuthenticationSchemeOptions, EfAuthenticationHandler>("EfAuth", null);

            builder.Services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    StringBuilder message = new();

                    foreach (var value in actionContext.ModelState.Values)
                    {
                        foreach (var error in value.Errors)
                        {
                            if (string.IsNullOrEmpty(error.ErrorMessage)) continue;
                            message.AppendLine(error.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(message.ToString());
                };
            });


            builder.Services.AddDbContext<BudgetDbContext>(
                opt =>
                {
                    opt.UseNpgsql(builder.Configuration.GetConnectionString("Data"),
                        o => o.MigrationsAssembly("Budget.Database"));
                    //opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    opt.EnableSensitiveDataLogging();
                });
        }
    }
}