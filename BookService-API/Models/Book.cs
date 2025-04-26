using System;
using System.Collections.Generic;

namespace BookService_API.Models;

public partial class Book
{
    public Guid BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Isbn { get; set; }

    public DateOnly? PublishedDate { get; set; }

    public decimal? Price { get; set; }

    public string? CoverImageUrl { get; set; }

    public Guid AuthorId { get; set; }

    public Guid GenreId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BookTag> BookTags { get; set; } = new List<BookTag>();

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
