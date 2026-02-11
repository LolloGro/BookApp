namespace BookApp;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public List<Quote> Quotes { get; set; } = new();
    public List<Book> Books { get; set; } = new();
}