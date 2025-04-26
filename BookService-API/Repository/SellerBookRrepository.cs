using BookService_API.Interface;
using BookService_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookService_API.Repository
{
    public class SellerBookRrepository : ISellerBookInterface
    {
        private readonly BookDatabseContext bookDatabseContext;

        public SellerBookRrepository(BookDatabseContext bookDatabseContext)
        {
            this.bookDatabseContext = bookDatabseContext;
        }


        public Task<bool> AddNewBookAsync(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            }else{
                bookDatabseContext.Books.Add(book);
                bookDatabseContext.SaveChanges();
                return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteBookAsync(Guid id)
        {
            var book = from books in bookDatabseContext.Books where books.BookId == id select books;
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book), "There is No such book to delete");
            }
            else{
                bookDatabseContext.Books.Remove(book.FirstOrDefault());
                bookDatabseContext.SaveChanges();
                return Task.FromResult(true);
            }
        }  
        public Task<List<Book>> GetAllBooksAsync()
        {
            return Task.FromResult(bookDatabseContext.Books.ToList());
        }

       public async Task<Guid> GetAuthorIdByName(string authorName)
        {
            var authorId = await bookDatabseContext.Authors
                .Where(a => a.Name == authorName)
                .Select(a => a.AuthorId)
                .FirstOrDefaultAsync();
    
            return authorId;
        }

        public async Task SaveAuthorName(string authorName)
        {
            bookDatabseContext.Authors.Add(new Author { Name = authorName });
            bookDatabseContext.SaveChanges();
        }

        public Task<bool> UpdateBookAsync(Guid id, Book book)
        {
            var existingBook= from books in bookDatabseContext.Books where books.BookId == id select books;
            if(existingBook == null)
            {
                throw new ArgumentNullException(nameof(existingBook), "There is No such book to update");
            }
            else{
                var bookToUpdate = existingBook.FirstOrDefault();
                bookToUpdate.Title = book.Title;
                bookToUpdate.Description = book.Description;
                bookToUpdate.PublishedDate = book.PublishedDate;
                bookToUpdate.Price = book.Price;
                bookToUpdate.CoverImageUrl = book.CoverImageUrl;
                bookDatabseContext.SaveChanges();
                return Task.FromResult(true);
            }
        }
    }
}
