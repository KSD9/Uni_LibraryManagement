using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_LibraryManagement.Data.Entities;

namespace Uni_LibraryManagement.Data
{
    public class AuthenticationService
    {
        public User LoggedUser { get; set; }
        private LibraryDbContext context;

        public void AuthenticateUser(string email, string password)
        {
            context = new LibraryDbContext();

            List<User> users = context.Users.Where((u) => u.Email == email && u.Password == password).ToList();

            this.LoggedUser = users.Count > 0 ? users[0] : null;
        }
    }
}
