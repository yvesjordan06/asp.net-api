

using System.ComponentModel.DataAnnotations;
using BankingServer.Auth.Domain.Models;

namespace BankingServer.Auth.Infrastructure.Data;

public class UserEntity : BaseDataEntity
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    
    public UserModel ToModel()
    {
        return new UserModel(Id, Password, Email);
        
    }
}