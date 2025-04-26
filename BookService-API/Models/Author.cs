using System;
using System.Collections.Generic;

namespace BookService_API.Models;

public partial class Author
{
    public Guid AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Bio { get; set; }

    public DateOnly? BirthDate { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Book> BooksNavigation { get; set; } = new List<Book>();
}
