using System.ComponentModel.DataAnnotations;

namespace BankingServer.Auth.Infrastructure.Data;

public class AuthTokenEntity : BaseDataEntity
{
     [Key]
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
    public UserEntity User { get; set; }
}