using BookApp.Models;

namespace BookApp.Services;

public class BookService : IBookService
{
    private readonly BookDb _database;
    
    public BookService(BookDb db)
        {
        _database = db;
        }

    public List<Book> GetAll()
    {
        return _database.Books.ToList();
    }

    public Book? GetById(int id)
    {
        return _database.Books.FirstOrDefault(b => b.Id == id);
    }

    public Book Create(Book book)
    {
        _database.Books.Add(book);
        _database.SaveChanges();
        return book; 
    }

    public void Update(int id, Book updatedBook)
    {
        var book =  _database.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            throw new KeyNotFoundException();
        }
        
        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.PublishDate = updatedBook.PublishDate;
        _database.SaveChanges();
    }

    public void Delete(int id)
    {
        var book =  _database.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            throw new KeyNotFoundException();
        }
        
        _database.Books.Remove(book);
        _database.SaveChanges();
    }
}