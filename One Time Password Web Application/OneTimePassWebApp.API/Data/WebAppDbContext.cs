using Microsoft.EntityFrameworkCore;
using OneTimePassWebApp.API.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace OneTimePassWebApp.API.Data
{
    public class WebAppDbContext: DbContext
    {
        public DbSet<Users> Users { get; set; }

        public WebAppDbContext([NotNullAttribute] DbContextOptions options): base(options)
        {
        }
    }
}
