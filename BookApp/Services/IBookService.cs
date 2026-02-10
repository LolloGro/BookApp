namespace BookApp.Services;

public interface IBookService
{
    List<Book> GetAll();
    Book? GetById(int id);
    Book Create(Book book);
    void Update(int id, Book book);
    void Delete(int id);
}