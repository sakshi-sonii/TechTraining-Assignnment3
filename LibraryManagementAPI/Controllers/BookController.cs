using LibraryManagementAPI.Models;
using LibraryManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _repository = new();

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_repository.GetAllBooks());
        }

        [HttpGet("{title}")]
        public IActionResult SearchBooksByTitle(string title)
        {
            var books = _repository.SearchBooksByTitle(title);
            if (!books.Any()) return NotFound("No books found matching the title.");

            return Ok(books);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (_repository.GetBookByISBN(book.ISBN) != null)
            {
                return Conflict("A book with the same ISBN already exists.");
            }

            _repository.AddBook(book);
            return CreatedAtAction(nameof(GetAllBooks), new { isbn = book.ISBN }, book);
        }

        [HttpPut("{isbn}")]
        public IActionResult UpdateBook(string isbn, [FromBody] Book updatedBook)
        {
            if (!_repository.UpdateBook(isbn, updatedBook))
            {
                return NotFound("Book not found with the given ISBN.");
            }

            return NoContent();
        }

        [HttpDelete("{isbn}")]
        public IActionResult RemoveBook(string isbn)
        {
            if (!_repository.RemoveBook(isbn))
            {
                return NotFound("Book not found with the given ISBN.");
            }

            return NoContent();
        }
    }
}
