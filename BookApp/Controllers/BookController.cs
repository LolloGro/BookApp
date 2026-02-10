using BookApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    //ApiController validates 
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }
        
        [HttpGet]
        // With ActionResult you define return of a status code 
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(_service.GetAll()); 
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _service.GetById(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book); 
        }

        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            var addedBook = _service.Create(book);
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, addedBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            try
            {
             _service.Update(id, updatedBook);
            }
            catch (KeyNotFoundException)
            {
               return NotFound(); 
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
