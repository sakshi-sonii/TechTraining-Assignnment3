using LibraryManagementAPI.Models;
using System.Xml.Linq;

namespace LibraryManagementAPI.Repositories
{
    public class BookRepository
    {
        private readonly List<Book> _books = new();

        public IEnumerable<Book> GetAllBooks() => _books;

        public Book? GetBookByISBN(string isbn) => _books.FirstOrDefault(b => b.ISBN == isbn);

        public IEnumerable<Book> SearchBooksByTitle(string title) =>
            _books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        public void AddBook(Book book) => _books.Add(book);

        public bool UpdateBook(string isbn, Book updatedBook)
        {
            var existingBook = GetBookByISBN(isbn);
            if (existingBook == null) return false;

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Available = updatedBook.Available;
            return true;
        }

        public bool RemoveBook(string isbn)
        {
            var book = GetBookByISBN(isbn);
            if (book == null) return false;

            _books.Remove(book);
            return true;
        }
    }
}
