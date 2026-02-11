using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services;

public class BookService(BookDb database, ICurrentUserService currentUser) : IBookService
{ 
    public async Task<List<BookDto>> GetAll()
    {
        return await database.Books
            .Where(u => u.UserId == currentUser.UserId)
            .Select(u => new BookDto
            {
                Id = u.Id,
                Title = u.Title,
                Author = u.Author,
                PublishDate = u.PublishDate
            }).ToListAsync(); 
    }

    public async Task<BookDto?> GetById(int id)
    {
        return await database.Books
            .Where(q => q.UserId == currentUser.UserId && q.Id == id)
            .Select(q => new BookDto
            {
                Id = q.Id,
                Title = q.Title,
                Author = q.Author,
                PublishDate = q.PublishDate
            }).FirstOrDefaultAsync(); 
    }

    public async Task<BookDto> Create(BookDto bookDto)
    {
        var book = new Book()
        {
            UserId = currentUser.UserId,
            Title = bookDto.Title,
            Author = bookDto.Author,
            PublishDate = bookDto.PublishDate
        };
        
        database.Books.Add(book);
        await database.SaveChangesAsync();

        var result = new BookDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublishDate = book.PublishDate
        };
        
        return result; 
    }

    public async Task Update(int id, BookDto updatedBook)
    {
        var book =  await database.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == currentUser.UserId);

        if (book == null)
        {
            throw new KeyNotFoundException();
        }
        
        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.PublishDate = updatedBook.PublishDate;
        await database.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await database.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == currentUser.UserId);

        if (book == null)
        {
            throw new KeyNotFoundException();
        }
        
        database.Books.Remove(book);
        await database.SaveChangesAsync();
    }
}