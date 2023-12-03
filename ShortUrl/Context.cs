using Microsoft.EntityFrameworkCore;
using ShortUrl.Models;
using System;

namespace ShortUrl
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;
        public Context() 
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            Database.Migrate();
        }
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MySqlConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<UrlModel> url { get; set; }
    }
}
