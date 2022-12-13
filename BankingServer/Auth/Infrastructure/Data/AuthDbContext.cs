
using BankingServer.Auth.Domain.Models;
using BankingServer.Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingServer.Auth.Infrastructure.Data
{
    
    
}

namespace BankingServer.Core
{
    public partial class DataContext: DbContext
    {
   
  
        public DbSet<AuthTokenEntity> Tokens { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    
    }


}


