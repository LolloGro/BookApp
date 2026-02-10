using BookApp.Models;

namespace BookApp.Services;

public class QuoteService(BookDb database) : IQuoteService

{
    public List<Quote> GetAll()
    {
        return database.Quotes.ToList();
    }

    public Quote? GetById(int id)
    {
        return database.Quotes.FirstOrDefault(q => q.Id == id);
    }

    public Quote Create(Quote quote)
    {
        database.Quotes.Add(quote);
        database.SaveChanges();
        return quote;
    }

    public void Update(int id, Quote updatedQuote)
    {
        var quote = database.Quotes.FirstOrDefault(q => q.Id == id);
        if (quote == null)
        {
            throw new KeyNotFoundException();
        }
        quote.QuoteText = updatedQuote.QuoteText;
        database.SaveChanges();
    }

    public void Delete(int id)
    {
        var quote = database.Quotes.FirstOrDefault(q => q.Id == id);

        if (quote == null)
        {
            throw new KeyNotFoundException();
        }
        database.Quotes.Remove(quote);
        database.SaveChanges();
    }
}