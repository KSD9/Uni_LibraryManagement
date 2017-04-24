namespace Uni_LibraryManagement.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Uni_LibraryManagement.Data.LibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Uni_LibraryManagement.Data.LibraryDbContext context)
        {
        }
    }
}
