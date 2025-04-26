using BookService_API.Interface;
using BookService_API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookService_API.Repository
{
    public class CustomerBookRepository : ICustomerBookInterface
    {
        private readonly BookDatabseContext bookDatabseContext;

        public CustomerBookRepository(BookDatabseContext bookDatabseContext)
        {
            this.bookDatabseContext = bookDatabseContext;
        }

        public Task addTocartAsync(string userId, Guid BookId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAllBooks()
        {
            var allBooks = (from book in bookDatabseContext.Books select book).ToListAsync();
            return allBooks;
        }

        public async Task<Book> GetBookById(Guid id)
        {
            var param = new SqlParameter("@book_id", SqlDbType.UniqueIdentifier)
            {
                Value = id
            };

            var books = await bookDatabseContext.Books
                .FromSqlRaw("EXEC GetBookByID_SP @book_id", param)
                .AsNoTracking()
                .ToListAsync(); // Materialize the query first

            return books.FirstOrDefault(); // Now apply FirstOrDefault safely
        }



        public Task<List<Book>> GetBooksByAuthor(string authorName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksByTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public Task RateBookAsync(string userId, Guid bookId, int score, string comment)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> SearchBooks(string searchTerm)
        {
            var param = new SqlParameter("@searchTerm", SqlDbType.NVarChar, 50)
            {
                Value = searchTerm
            };

            var books = bookDatabseContext.Books
                .FromSqlRaw("EXEC SearchBooks_SP @searchTerm", param)
                .AsNoTracking()
                .ToListAsync(); // Materialize the query first

            return books; // Now apply FirstOrDefault safely
        }
    }
}
