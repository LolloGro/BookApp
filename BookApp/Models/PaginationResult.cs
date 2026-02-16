namespace BookApp.Models;

public class PaginationResult<T>
{
    public List<T> ListItems { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; } = 5;
}