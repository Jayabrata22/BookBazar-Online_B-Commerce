using BookService_API.DTO;
using BookService_API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookService_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerBookController : ControllerBase
    {
        private readonly BookService bookService;

        public SellerBookController( BookService bookService){
            this.bookService = bookService;
        }

        [HttpPost("AddNewBook")]
        public async Task<IActionResult> AddNewBookAsync([FromBody] SellerBookDTO book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null");
            }

            var result = await this.bookService.AddNewBookAsync(book);
            if (result == false)
            {
                return BadRequest("Failed to add new book");
            }

            return Ok("Book added successfully");
        }

        
        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] SellerBookDTO book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null");
            }

            var result = await this.bookService.UpdateBookAsync(id, book);
            if (result == false)
            {
                return BadRequest("Failed to update book");
            }

            return Ok("Book updated successfully");
        }
        
        
        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            var result = await this.bookService.DeleteBookAsync(id);
            if (result == false)
            {
                return BadRequest("Failed to delete book");
            }

            return Ok("Book deleted successfully");
        }
        
        
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await this.bookService.getAllBooks();
            if (books == null || books.Count == 0)
            {
                return NotFound("No books found");
            }

            return Ok(books);
        }





    }
}
