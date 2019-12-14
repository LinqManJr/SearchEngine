using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SearchEngine.Domain.Context
{
    public class SearchContext : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            builder.ApplyConfiguration(new ResultConfiguration());
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); \\add all configurations
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
