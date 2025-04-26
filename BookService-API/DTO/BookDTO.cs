namespace BookService_API.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }

        public string? Description { get; set; }
        public DateOnly? PublishedDate { get; set; }

        public decimal? Price { get; set; }

        public string? CoverImageUrl { get; set; }

        public string AuthorName { get; set; }
    }
}
