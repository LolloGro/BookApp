namespace BookApp.Services;

public interface IQuoteService
{
    List<Quote> GetAll();
    Quote? GetById(int id);
    Quote Create(Quote quote);
    void Update(int id, Quote quote);
    void Delete(int id);
}