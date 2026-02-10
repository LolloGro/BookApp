using Microsoft.EntityFrameworkCore;

namespace BookApp.Models;

public class BookDb(DbContextOptions<BookDb> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    
}