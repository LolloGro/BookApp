namespace BookApp.Services;

public interface IBookService
{
    Task<List<BookDto>> GetAll();
    Task<BookDto?> GetById(int id);
    Task<BookDto> Create(BookDto book);
    Task Update(int id, BookDto book);
    Task Delete(int id);
}