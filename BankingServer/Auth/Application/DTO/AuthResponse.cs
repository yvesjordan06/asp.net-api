using BankingServer.Auth.Domain.Models;

namespace BankingServer.Auth.Application.DTO
{
    public struct AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        
        
        //Tranformer
        public static AuthResponse FromAuthTokenModel(AuthTokenModel model)
        {
            return new AuthResponse
            {
                Token = model.Token,
                RefreshToken = "RefreshToken"
            };
        }
    }
    
    
}
