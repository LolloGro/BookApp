namespace BookApp;

public class Book
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public DateTime PublishDate { get; set; }

    public User User { get; set; } = null!;
}