namespace BookApp;

public class Quote
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? QuoteText { get; set; } = null!;
    
    public User User { get; set; } = null!;
}