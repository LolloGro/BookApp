namespace BookApp.Services;

public interface IQuoteService
{
    Task<List<QuoteDto>> GetAll();
    Task<QuoteDto?> GetById(int id);
    Task<QuoteDto> Create(QuoteDto quoteDto);
    Task Update(int id, QuoteDto quoteDto);
    Task Delete(int id);
}