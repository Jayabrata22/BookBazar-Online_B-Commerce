using BookService_API.DTO;
using BookService_API.Interface;
using BookService_API.Models;

namespace BookService_API.Service
{
    public class BookService
    {
        private readonly ICustomerBookInterface customerBookInterface;
        private readonly ISellerBookInterface sellerBookInterface;

        public BookService(ICustomerBookInterface customerBookInterface, ISellerBookInterface sellerBookInterface)
        {
            this.customerBookInterface = customerBookInterface;
            this.sellerBookInterface = sellerBookInterface;
        }
       
        //Customer Book Service Methods:
        //Get All Books:
        public async Task<List<BookDTO>> getAllBooks()
        {
            var allBooks= await this.customerBookInterface.GetAllBooks();
            string authorname = 'e'.ToString();
            var BookDtoList = allBooks.Select(book => new BookDTO
            {
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Price = book.Price,
                CoverImageUrl = book.CoverImageUrl,
                AuthorName = authorname
            }).ToList();
            return BookDtoList;

        }
        //Get Book By Id:
        public async Task<BookDTO> getRequestedBookbyId(Guid BookId)
        {
            var getBook = await this.customerBookInterface.GetBookById(BookId);
            return new BookDTO { Title = getBook.Title, Description = getBook.Description, PublishedDate = getBook.PublishedDate, Price = getBook.Price, CoverImageUrl = getBook.CoverImageUrl };
        }
        //Search Book:
        public async Task<List<BookDTO>> searchBook(string searchString)
        {
            var searchResult = await this.customerBookInterface.SearchBooks(searchString);
            var BookDtoList = searchResult.Select(book => new BookDTO
            {
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Price = book.Price,
                CoverImageUrl = book.CoverImageUrl
            }).ToList();
            return BookDtoList;
        }


        //Call AddtoCart Service from CustomerBookService:
        public Task<string> AddTocartAsync(string userId, Guid BookId, int quantity)
        {
            
            return Task.FromResult(true.ToString());
        }


        //Seller Book Service Methods:
        //Add New Book:
        public async Task<bool> AddNewBookAsync(SellerBookDTO book)
        {
            //SaveAuthorName(book.AuthorName);
            var AuthorId = GetAuthorIdByName(book.AuthorName);

            var newBook = new Book
            {
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Isbn = book.Isbn,
                Price = book.Price,
                AuthorId = await AuthorId,
                CoverImageUrl = book.CoverImageUrl
            };
            return await this.sellerBookInterface.AddNewBookAsync(newBook);
        }
        //Get Author Id by Name:
        private async Task<Guid> GetAuthorIdByName(string? authorName)
        {
            return await sellerBookInterface.GetAuthorIdByName(authorName);
        }

        //Save Author Name:
        private async void SaveAuthorName(string? authorName)
        {
            var exsistingAuthor = sellerBookInterface.GetAuthorIdByName(authorName);
            if (exsistingAuthor == null)
            {
                await sellerBookInterface.SaveAuthorName(authorName);
            }
            else
            {
                return;
            }
        }
        //Update Book:
        public async Task<bool> UpdateBookAsync(Guid id, SellerBookDTO book)
        {
            var updatedBook = new Book
            {
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Price = book.Price,
                CoverImageUrl = book.CoverImageUrl
            };
            return await this.sellerBookInterface.UpdateBookAsync(id, updatedBook);
        }

        //Delete Book:
        public async Task<bool> DeleteBookAsync(Guid id)
        {
            return await this.sellerBookInterface.DeleteBookAsync(id);
        }

        
    }
}
