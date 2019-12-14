using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Domain.Context
{
    public class SearchContext : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options) : base(options)
        {

        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
