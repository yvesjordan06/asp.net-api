using BankingServer.Auth.Domain.Models;

namespace BankingServer.Auth.Domain.Repositories;

    public interface IAuthRepository
    {
        Task<UserModel> GetUserByID(string id);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserByToken(string token);
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> UpdateUser(string id, UserModel user);
        Task<UserModel> DeleteUser(string id);
        Task<IEnumerable<UserModel>> GetUsers();
    
        //Tokens
        Task<AuthTokenModel> GetToken(string token);
        Task<AuthTokenModel> CreateTokenForUser(string userId, string token);
        Task<AuthTokenModel> DeleteToken(string token);
        Task<IEnumerable<AuthTokenModel>> GetTokensForUser(string userId);
    }
