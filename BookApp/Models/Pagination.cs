namespace BookApp.Models;

public class Pagination
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public void CheckPagination()
    {
        if (Page < 1) Page = 1;
        if (PageSize < 1) PageSize = 5;
        if (PageSize > 5) PageSize = 5;
    }
}