namespace BookService_API.DTO
{
    public class SellerBookDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? PublishedDate { get; set; }
        public string? Isbn { get; set; }
        public decimal? Price { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? GenereName { get; set; }
        public string? AuthorName { get; set; }
    }
}
