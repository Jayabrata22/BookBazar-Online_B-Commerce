using BookService_API.Models;

namespace BookService_API.Interface
{
    public interface ICustomerBookInterface
    {
        //Get All books in UI
        Task<List<Book>> GetAllBooks();
        //View Book Details:
        Task<Book> GetBookById(Guid id);
        //Browse Categories/Genres/Author/Tags:
        Task<List<Book>> GetBooksByAuthor(string authorName);
        Task<List<Book>> GetBooksByGenre(string genreName);
        Task<List<Book>> GetBooksByTag(string tagName);
        //Search Books:
        Task<List<Book>> SearchBooks(string searchTerm);
        Task addTocartAsync(string userId,Guid BookId,int quantity);
        Task RateBookAsync(string userId, Guid bookId, int score, string comment);



    }
}
