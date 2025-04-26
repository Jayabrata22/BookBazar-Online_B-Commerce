using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookService_API.Models;

public partial class BookDatabseContext : DbContext
{
    public BookDatabseContext()
    {
    }

    public BookDatabseContext(DbContextOptions<BookDatabseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookTag> BookTags { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=BookBaazar_BookDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC345A282E0D");

            entity.Property(e => e.AuthorId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207775BB69A");

            entity.Property(e => e.BookId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CoverImageUrl).HasMaxLength(500);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Authors");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Genres");

            entity.HasMany(d => d.Authors).WithMany(p => p.BooksNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAutho__Autho__4CA06362"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAutho__BookI__4BAC3F29"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("PK__BookAuth__6AED6DC4B3ACB28F");
                        j.ToTable("BookAuthors");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.BooksNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookGenre__Genre__5070F446"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookGenre__BookI__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("BookId", "GenreId").HasName("PK__BookGenr__CDD89250639DD24B");
                        j.ToTable("BookGenres");
                    });
        });

        modelBuilder.Entity<BookTag>(entity =>
        {
            entity.HasKey(e => e.BookTagId).HasName("PK__BookTags__BFFCA534D589BC4A");

            entity.Property(e => e.BookTagId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Tag).HasMaxLength(50);

            entity.HasOne(d => d.Book).WithMany(p => p.BookTags)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookTags_Books");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057E4DCACA1E");

            entity.Property(e => e.GenreId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF87C59630495");

            entity.Property(e => e.RatingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.RatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Book).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ratings_Books");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
