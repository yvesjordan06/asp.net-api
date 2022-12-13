using BankingServer.Auth.Application.Services;
using BankingServer.Auth.Domain.Repositories;
using BankingServer.Auth.Domain.Services;
using BankingServer.Auth.Infrastructure;
using BankingServer.Auth.Infrastructure.Repositories;
using BankingServer.Core;
using Microsoft.EntityFrameworkCore;

namespace BankingServer.Auth
{
    public class AuthServiceRegistrar
    {
        public static  void Register(IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IAuthRepository, AuthRepository>();
        
        }
    }
}