namespace BookApp.Models;

public class PaginationResult<T>
{
    public List<T> ListItems { get; set; } = new List<T>();
    public int TotalCount { get; set; }
}