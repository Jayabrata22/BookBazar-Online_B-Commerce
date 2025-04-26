using System;
using System.Collections.Generic;

namespace BookService_API.Models;

public partial class BookTag
{
    public Guid BookTagId { get; set; }

    public Guid BookId { get; set; }

    public string Tag { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
