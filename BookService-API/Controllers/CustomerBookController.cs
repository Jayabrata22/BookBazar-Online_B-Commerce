using BookService_API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookService_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBookController : ControllerBase
    {
        private readonly BookService bookService;

        public CustomerBookController(BookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllbookAsync()
        {
            var listOfBooks =await this.bookService.getAllBooks();
            if(listOfBooks == null)
            {
                return NotFound();
            }
            return Ok(listOfBooks);
        }

        [HttpGet("Book/{id}")]
        public async Task<IActionResult> GetBookbyId(Guid BookId)
        {
            var requstedBook=await this.bookService.getRequestedBookbyId(BookId);
            if(requstedBook == null)
            {
                return NotFound();
            }
            return Ok(requstedBook);
        }

        [HttpGet("Book/Search/{searchString}")]
        public async Task<IActionResult> SearchBook(string searchString){
            var searchResult = await this.bookService.searchBook(searchString);
            if(searchResult == null)
            {
                return NotFound();
            }
            return Ok(searchResult);
        }

        [HttpPost("AddToCart/{userId}/{BookId}/{quantity}")]
        public async Task<IActionResult> AddToCart(string userId, Guid BookId, int quantity)
        {
            var result = await this.bookService.AddTocartAsync(userId, BookId, quantity);
            if (result == null)
            {
                return NotFound();
            }
          return Ok(result);
        }
    }
}
