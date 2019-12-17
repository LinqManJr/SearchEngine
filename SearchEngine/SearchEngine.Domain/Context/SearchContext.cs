using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Models;

namespace SearchEngine.Domain.Context
{
    public class SearchContext : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options) : base(options) { }             

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Request>().Property(d => d.Date).HasDefaultValueSql("getdate()");
            builder.ApplyConfiguration(new ResultConfiguration());            
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
