using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    //ApiController validates 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookService service) : ControllerBase
    {
        [HttpGet]
        // With ActionResult you define return of a status code 
        public async Task<ActionResult<List<BookDto>>> GetBooks()
        {
            return Ok(await service.GetAll()); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto?>> GetBookById(int id)
        {
            var book = await service.GetById(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book); 
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook(BookDto book)
        {
            var addedBook = await service.Create(book);
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, addedBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto updatedBook)
        {
            try
            {
             await service.Update(id, updatedBook);
            }
            catch (KeyNotFoundException)
            {
               return NotFound(); 
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
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
