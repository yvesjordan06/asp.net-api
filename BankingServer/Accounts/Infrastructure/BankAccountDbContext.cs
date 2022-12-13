using BankingServer.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingServer.Accounts.Infrastructure
{
    
}

namespace BankingServer.Core
{
    public partial class DataContext: DbContext
    {
   
        public DbSet<BankAccountModel> BankAccounts { get; set; } = null!;

   
    }
}