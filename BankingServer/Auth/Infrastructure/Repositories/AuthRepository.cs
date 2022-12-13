using BankingServer.Auth.Domain.Models;
using BankingServer.Auth.Domain.Repositories;

namespace BankingServer.Auth.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
    
        private readonly DataContext _db;

        public AuthRepository(DataContext context)
        {
            _db = context;
        }
    
        public Task<UserModel> GetUserByID(string id)
        {
            throw BankException.NotImplemented();
        }

        public Task<UserModel> GetUserByEmail(string email)
        {
            throw BankException.NotImplemented();
        }

        public Task<UserModel> GetUserByToken(string token)
        {
            throw BankException.NotImplemented();
        }

        public Task<UserModel> CreateUser(UserModel user)
        {
            throw BankException.NotImplemented();
        }

        public Task<UserModel> UpdateUser(string id, UserModel user)
        {
            throw BankException.NotImplemented();
        }

        public Task<UserModel> DeleteUser(string id)
        {
            throw BankException.NotImplemented();
        }

        public Task<IEnumerable<UserModel>> GetUsers()
        {
            throw BankException.NotImplemented();
        }

        public Task<AuthTokenModel> GetToken(string token)
        {
            throw BankException.NotImplemented();
        }

        public Task<AuthTokenModel> CreateTokenForUser(string userId, string token)
        {
            throw BankException.NotImplemented();
        }

        public Task<AuthTokenModel> DeleteToken(string token)
        {
            throw BankException.NotImplemented();
        }

        public Task<IEnumerable<AuthTokenModel>> GetTokensForUser(string userId)
        {
            throw BankException.NotImplemented();
        }
    }
}