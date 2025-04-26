using System;
using System.Collections.Generic;

namespace BookService_API.Models;

public partial class Rating
{
    public Guid RatingId { get; set; }

    public Guid BookId { get; set; }

    public string UserId { get; set; } = null!;

    public int? Score { get; set; }

    public string? Comment { get; set; }

    public DateTime? RatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;
}
