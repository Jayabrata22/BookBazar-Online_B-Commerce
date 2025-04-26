using System;
using System.Collections.Generic;

namespace BookService_API.Models;

public partial class Genre
{
    public Guid GenreId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Book> BooksNavigation { get; set; } = new List<Book>();
}
