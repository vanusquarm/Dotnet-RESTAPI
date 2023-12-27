using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.API.Entities.Models;

namespace WebApplication.API.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Mapping Database tables
        public DbSet<TodoItem> TodoItems { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<TodoItem>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "swn";
                        break;
                    // case EntityState.Modified:
                    //     entry.Entity.LastModifiedDate = DateTime.Now;
                    //     entry.Entity.LastModifiedBy = "swn";
                    //     break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
