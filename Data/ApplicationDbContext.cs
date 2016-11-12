using AccountsWebsite.Models;
using KennedyLabsWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace AccountsWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PageModel> Pages { get; set; }

        public DbSet<SectionModel> Sections { get; set; }

        public DbSet<ItemModel> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsIntraHierarchical()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
