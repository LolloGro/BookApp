using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController(IQuoteService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<QuoteDto>>> GetQuotes()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteDto>> GetQuoteById(int id)
        {
            var quote = await service.GetById(id);
            if (quote == null)
            {
                return NotFound();
            }
            return Ok(quote);
        }

        [HttpPost]
        public async Task<ActionResult<QuoteDto>> AddQuote(QuoteDto quote)
        {
            var addedQuote = await service.Create(quote);
            return CreatedAtAction(nameof(GetQuoteById), new { id = addedQuote.Id }, addedQuote);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuote(int id, QuoteDto quote)
        {
            try
            {
               await service.Update(id, quote);
            }
            catch (KeyNotFoundException)
            {
                return  NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            try
            {
                await service.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
