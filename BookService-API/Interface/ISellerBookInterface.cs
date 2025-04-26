using BookService_API.Models;
 
namespace BookService_API.Interface
{
    public interface ISellerBookInterface
    {
        //Add New Book:
        Task<bool> AddNewBookAsync(Book book);
        //Update Book:
        Task<bool> UpdateBookAsync(Guid id, Book book);
        //Delete Book:
        Task<bool> DeleteBookAsync(Guid id);
        //Get All Books:
        Task<List<Book>> GetAllBooksAsync();

        Task SaveAuthorName(string authorName);
        Task<Guid> GetAuthorIdByName(string authorName);

    }
}