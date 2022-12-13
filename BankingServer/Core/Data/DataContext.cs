using BankingServer.Core.Data;
using Microsoft.EntityFrameworkCore;

// BankingServer.Core.DataContext
namespace BankingServer.Core
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Register the database context
        /// This is the only place where the database context is registered
        /// </summary>
        /// <param name="service">The Service Injector</param>
        ///
        /// <example>
        /// In the Program.cs file\n
        /// DataContext.Register(builder.Services);
        /// </example>
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"))
            );
        }

        //Override the Methode SaveChanges to Added the Dates to the BaseDataEntity
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseDataEntity entity)

                {
                    entity.ModifiedDate = DateTime.Now;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                        case EntityState.Modified:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}