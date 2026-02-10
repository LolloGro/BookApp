using BookApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController(IQuoteService service) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Quote>> GetQuotes()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Quote> GetQuoteById(int id)
        {
            var quote = service.GetById(id);
            if (quote == null)
            {
                return NotFound();
            }
            return Ok(quote);
        }

        [HttpPost]
        public ActionResult<Quote> AddQuote(Quote quote)
        {
            var addedQuote = service.Create(quote);
            return CreatedAtAction(nameof(GetQuoteById), new { id = addedQuote.Id }, addedQuote);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateQuote(int id, Quote quote)
        {
            try
            {
                service.Update(id, quote);
            }
            catch (KeyNotFoundException)
            {
                return  NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuote(int id)
        {
            try
            {
                service.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
