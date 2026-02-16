using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services;

public class QuoteService(BookDb database, ICurrentUserService currentUser) : IQuoteService

{
    public async Task<PaginationResult<QuoteDto>> GetAll(Pagination pagination)
    {
        var query = database.Quotes
            .Where(u => u.UserId == currentUser.UserId)
            .AsQueryable();
        
        var totalQuote = await query.CountAsync();
        
        var quotes = await query
            .OrderBy(q => q.Id)
            .Skip((pagination.Page -1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        var convertedQuotes = quotes.Select(q => new QuoteDto
        {
            Id = q.Id,
            QuoteText = q.QuoteText
        }).ToList();

        return new PaginationResult<QuoteDto>
        {
            ListItems = convertedQuotes,
            TotalCount = totalQuote,
            Page = pagination.Page,
            PageSize = pagination.PageSize
        };
    }

    //returnerar null om boken tillhör någon annan eller inte finns 
    public async Task<QuoteDto?> GetById(int id)
    {
        return await  database.Quotes
            .Where(q => q.UserId == currentUser.UserId && q.Id == id)
            .Select(quote => new QuoteDto
            {
                Id = quote.Id,
                QuoteText = quote.QuoteText
            }
            ).FirstOrDefaultAsync();
    }

    public async Task<QuoteDto> Create(QuoteDto quoteDto)
    {
        var quote = new Quote
        {
            UserId = currentUser.UserId,
            QuoteText = quoteDto.QuoteText
        };
        
        database.Quotes.Add(quote);
        await database.SaveChangesAsync();

        var result = new QuoteDto
        {
            Id = quote.Id,
            QuoteText = quote.QuoteText
        }; 
        
        return result;
    }

    public async Task Update(int id, QuoteDto updatedQuote)
    {
        var quote = await database.Quotes.FirstOrDefaultAsync(q => q.Id == id && q.UserId == currentUser.UserId);
        
        if (quote == null)
        {
            throw new KeyNotFoundException();
        }
        
        quote.QuoteText =  updatedQuote.QuoteText;
        await database.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var quote = await database.Quotes.FirstOrDefaultAsync(q => q.Id == id && q.UserId == currentUser.UserId);

        if (quote == null)
        {
            throw new KeyNotFoundException();
        }
        database.Quotes.Remove(quote);
        await database.SaveChangesAsync();
    }
}