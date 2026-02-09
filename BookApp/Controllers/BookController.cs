using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    //ApiController validates 
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        static private List<Book> _books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Mormor hälsar och säger förlåt",
                Author = "Fredrik Backman",
                PublishDate = DateTime.Now,
            },
            new Book
            {
                Id = 2,
                Title = "En man som heter Ove",
                Author = "Fredrik Backman",
                PublishDate = DateTime.Now,
            },
            new Book
            {
                Id = 3,
                Title = "Folk med ångest",
                Author = "Fredrik Backman",
                PublishDate = DateTime.Now,
            }
        };

        [HttpGet]
        // With ActionResult you define return of a status code 
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(_books); 
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book =  _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book); 
        }

        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book =  _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishDate = updatedBook.PublishDate;
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book =  _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            
            _books.Remove(book);
            return NoContent();
        }
    }
}
