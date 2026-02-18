using System.Text;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpContextAccessor();

// Connection to database 
builder.Services.AddDbContext<BookDb>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 45)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var jwtKey = builder.Configuration["JwtKey"] ?? throw new Exception("Jwt key missing");

builder.Services.AddAuthentication(auth =>
    {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        var key = Encoding.UTF8.GetBytes(jwtKey);
        
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("angular", policy =>
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
});

var app = builder.Build();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("angular");
//app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();