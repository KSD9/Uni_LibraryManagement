namespace Uni_LibraryManagement.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Uni_LibraryManagement.Data.Entities;

    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
            : base("name=LibraryDbContext")
        {
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<BookIssue> BookIssues { get; set; }
    }
    
}