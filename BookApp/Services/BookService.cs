using BookApp.Models;

namespace BookApp.Services;

public class BookService(BookDb database) : IBookService
{ 
    public List<Book> GetAll()
    {
        return database.Books.ToList();
    }

    public Book? GetById(int id)
    {
        return database.Books.FirstOrDefault(b => b.Id == id);
    }

    public Book Create(Book book)
    {
        database.Books.Add(book);
        database.SaveChanges();
        return book; 
    }

    public void Update(int id, Book updatedBook)
    {
        var book =  database.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            throw new KeyNotFoundException();
        }
        
        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.PublishDate = updatedBook.PublishDate;
        database.SaveChanges();
    }

    public void Delete(int id)
    {
        var book =  database.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            throw new KeyNotFoundException();
        }
        
        database.Books.Remove(book);
        database.SaveChanges();
    }
}