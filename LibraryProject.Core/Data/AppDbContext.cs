using System;
using System.Collections.Generic;
using LibraryProject.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Core.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Borrowing> Borrowings { get; set; }

    public virtual DbSet<BorrowingRule> BorrowingRules { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=localhost:1521/XEPDB1;User Id=Library_Project;Password=12345678;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("LIBRARY_PROJECT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("AUTHORS");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("AUTHOR_ID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AUTHOR_NAME");
            entity.Property(e => e.Biography)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("BIOGRAPHY");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("BOOKS");

            entity.HasIndex(e => e.CategoryId, "IDX_BOOKS_CAT_ID");

            entity.HasIndex(e => e.PublisherId, "IDX_BOOKS_PUB_ID");

            entity.HasIndex(e => e.Isbn, "UQ_BOOK_ISBN").IsUnique();

            entity.Property(e => e.BookId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("BOOK_ID");
            entity.Property(e => e.AvailableCopies)
                .HasDefaultValueSql("1 ")
                .HasColumnType("NUMBER")
                .HasColumnName("AVAILABLE_COPIES");
            entity.Property(e => e.CategoryId)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CoverImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("COVER_IMAGE");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE ")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.IsFeatured)
                .IsRequired()
                .HasDefaultValueSql("0 ")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("IS_FEATURED");
            entity.Property(e => e.Isbn)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.PublicationYear)
                .HasPrecision(4)
                .HasColumnName("PUBLICATION_YEAR");
            entity.Property(e => e.PublisherId)
                .HasColumnType("NUMBER")
                .HasColumnName("PUBLISHER_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TITLE");
            entity.Property(e => e.TotalCopies)
                .HasDefaultValueSql("1 ")
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL_COPIES");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BOOK_CATEGORY");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BOOK_PUBLISHER");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BA_AUTHOR"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BA_BOOK"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("BOOK_AUTHORS");
                        j.HasIndex(new[] { "AuthorId" }, "IDX_BA_AUTHOR_ID");
                        j.IndexerProperty<decimal>("BookId")
                            .HasColumnType("NUMBER")
                            .HasColumnName("BOOK_ID");
                        j.IndexerProperty<decimal>("AuthorId")
                            .HasColumnType("NUMBER")
                            .HasColumnName("AUTHOR_ID");
                    });
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.ToTable("BORROWINGS");

            entity.HasIndex(e => e.ApprovedBy, "IDX_BORROW_APPROVED_BY");

            entity.HasIndex(e => e.BookId, "IDX_BORROW_BOOK_ID");

            entity.HasIndex(e => e.MemberId, "IDX_BORROW_MEMBER_ID");

            entity.HasIndex(e => e.Status, "IDX_BORROW_STATUS");

            entity.Property(e => e.BorrowingId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("BORROWING_ID");
            entity.Property(e => e.ActualReturnDate)
                .HasColumnType("DATE")
                .HasColumnName("ACTUAL_RETURN_DATE");
            entity.Property(e => e.ApprovedBy)
                .HasColumnType("NUMBER")
                .HasColumnName("APPROVED_BY");
            entity.Property(e => e.BookId)
                .HasColumnType("NUMBER")
                .HasColumnName("BOOK_ID");
            entity.Property(e => e.BorrowDate)
                .HasColumnType("DATE")
                .HasColumnName("BORROW_DATE");
            entity.Property(e => e.ExpectedReturnDate)
                .HasColumnType("DATE")
                .HasColumnName("EXPECTED_RETURN_DATE");
            entity.Property(e => e.FineAmount)
                .HasDefaultValueSql("0 ")
                .HasColumnType("NUMBER(8,2)")
                .HasColumnName("FINE_AMOUNT");
            entity.Property(e => e.FineStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'None' ")
                .HasColumnName("FINE_STATUS");
            entity.Property(e => e.MemberId)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBER_ID");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOTES");
            entity.Property(e => e.PaidAt)
                .HasColumnType("DATE")
                .HasColumnName("PAID_AT");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("SYSDATE ")
                .HasColumnType("DATE")
                .HasColumnName("REQUEST_DATE");
            entity.Property(e => e.RuleId)
                .HasColumnType("NUMBER")
                .HasColumnName("RULE_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("'Pending' ")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_BORROW_ADMIN");

            entity.HasOne(d => d.Book).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BORROW_BOOK");

            entity.HasOne(d => d.Member).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BORROW_MEMBER");

            entity.HasOne(d => d.Rule).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BORROW_RULE");
        });

        modelBuilder.Entity<BorrowingRule>(entity =>
        {
            entity.HasKey(e => e.RuleId);

            entity.ToTable("BORROWING_RULES");

            entity.Property(e => e.RuleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("RULE_ID");
            entity.Property(e => e.FinePerDay)
                .HasDefaultValueSql("1.00 ")
                .HasColumnType("NUMBER(6,2)")
                .HasColumnName("FINE_PER_DAY");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("1 ")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.MaxBooksPerMember)
                .HasDefaultValueSql("3 ")
                .HasColumnType("NUMBER")
                .HasColumnName("MAX_BOOKS_PER_MEMBER");
            entity.Property(e => e.MaxBorrowDays)
                .HasDefaultValueSql("14 ")
                .HasColumnType("NUMBER")
                .HasColumnName("MAX_BORROW_DAYS");
            entity.Property(e => e.RuleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'Standard Policy' ")
                .HasColumnName("RULE_NAME");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("CATEGORIES");

            entity.HasIndex(e => e.CategoryName, "UQ_CATEGORY_NAME").IsUnique();

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("LOGINS");

            entity.HasIndex(e => e.RoleId, "IDX_LOGINS_ROLE_ID");

            entity.HasIndex(e => e.MemberId, "UQ_LOGIN_MEMBER").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_LOGIN_USERNAME").IsUnique();

            entity.Property(e => e.LoginId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("LOGIN_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE ")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.MemberId)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBER_ID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Member).WithOne(p => p.Login)
                .HasForeignKey<Login>(d => d.MemberId)
                .HasConstraintName("FK_LOGIN_MEMBER");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGIN_ROLE");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("MEMBERS");

            entity.HasIndex(e => e.Email, "UQ_MEMBER_EMAIL").IsUnique();

            entity.Property(e => e.MemberId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MEMBER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FULL_NAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("SYSDATE ")
                .HasColumnType("DATE")
                .HasColumnName("REGISTRATION_DATE");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'Active' ")
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("PUBLISHERS");

            entity.Property(e => e.PublisherId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PUBLISHER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PUBLISHER_NAME");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("ROLES");

            entity.HasIndex(e => e.RoleName, "UQ_ROLE_NAME").IsUnique();

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
