using Microsoft.EntityFrameworkCore;

namespace BookApp.Models;

public class BookDb(DbContextOptions<BookDb> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<User> Users => Set<User>();

    //Om en användare tas bort tas även all tillhörande data till användaren bort 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Book>()
            .HasOne(a => a.User)
            .WithMany(b => b.Books)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Quote>()
            .HasOne(a => a.User)
            .WithMany(b => b.Quotes)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

