using BankingServer.Auth.Application.Controllers;
using BankingServer.Auth.Domain.Models;

namespace BankingServer.Auth.Domain.Services
{
    public interface IAuthService
    {

        Task<UserModel> RegisterAsync(string email, string password);
        Task<AuthTokenModel> LoginAsync(string email, string password);
        Task<AuthTokenModel> RefreshTokenAsync(string token, string refreshToken);

        Task<string> Predict(Stream images);
        Task LogoutAsync();
        Task<QnaModel> AskQuestion(String question);
        
        Task<string> Translate(string text, string to);
    }
}