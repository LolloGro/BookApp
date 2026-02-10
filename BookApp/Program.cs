using BookApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Connection to database 
builder.Services.AddDbContext<BookDb>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 45)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

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