namespace BankingServer.Auth.Domain.Models
{
    public class AuthTokenModel
    {
        public int Id { get;  }
        public string Token { get; private set; }
        public DateTime Expiration { get; private set; }
        public UserModel User { get; set; }
        
        public AuthTokenModel(int id, UserModel user, string token, DateTime expiration)
        {
            Id = id;
            User = user;
            Token = token;
            Expiration = expiration;
        }
       
        
        //This function tells if the token has expired
        public bool IsExpired()
        {
            return DateTime.UtcNow >= Expiration;
        }
        
        //This function generates a new token
        public void GenerateToken()
        {
            Token = Guid.NewGuid().ToString();
            Expiration = DateTime.UtcNow.AddHours(1);
        }
        
        //This function refreshes the token
        public void RefreshToken()
        {
            Expiration = DateTime.UtcNow.AddHours(1);
        }
    }
}
