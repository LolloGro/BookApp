using BookApp.Models;

namespace BookApp.Services;

public interface IQuoteService
{
    Task<PaginationResult<QuoteDto>> GetAll(Pagination pagination);
    Task<QuoteDto?> GetById(int id);
    Task<QuoteDto> Create(QuoteDto quoteDto);
    Task Update(int id, QuoteDto quoteDto);
    Task Delete(int id);
}