using BookApp;
using BookApp.Models;
using BookApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection to database 
builder.Services.AddDbContext<BookDb>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 45)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()));


builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Replaces 'dotnet ef database update' and automatically creates tables in database when entering 'docker compose up --build'   
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookDb>();
    
    var retries = 10;

    for (int i = 0; i < retries; i++)
    {
        try
        {
            db.Database.Migrate();

            if (!db.Books.Any())
            {
                db.Books.AddRange(
                    new Book
                    {
                        Title = "Mormor hälsar och säger förlåt", Author = "Fredrik Backman", PublishDate = DateTime.Now
                    },
                    new Book { Title = "En man som heter Ove", Author = "Fredrik Backman", PublishDate = DateTime.Now },
                    new Book { Title = "Folk med ångest", Author = "Fredrik Backman", PublishDate = DateTime.Now });
                db.SaveChanges();
            }

            if (!db.Quotes.Any())
            {
                db.Quotes.AddRange(
                    new Quote { QuoteText = "Ta igen skit" });
                db.SaveChanges();
            }
            
            Console.WriteLine("Database migrated"); 
            break;
        }
        catch (MySqlConnector.MySqlException)
        { 
            Console.WriteLine("Database migration failed, retrying... in 5 seconds"); 
            Thread.Sleep(5000); 
        } 
    }
}

app.MapControllers();

app.Run();