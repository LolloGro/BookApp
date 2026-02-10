using BookApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookApp.Data;
//Is used by dotnet ef to manage database design, without starting the full application
//Uses localhost:5000
public class BookDbFactory : IDesignTimeDbContextFactory<BookDb>
{
    public BookDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookDb>();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connectionString = config.GetConnectionString("DefaultConnection");

        optionsBuilder.UseMySql(
            connectionString, 
            ServerVersion.AutoDetect(connectionString),
            mySqlOptions => mySqlOptions.EnableRetryOnFailure());
        
        return new BookDb(optionsBuilder.Options);
    }
}