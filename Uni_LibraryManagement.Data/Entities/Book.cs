using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_LibraryManagement.Data.Enumerations;

namespace Uni_LibraryManagement.Data.Entities
{
    public class Book : BaseEntity
    {
        public string ISBN { get; set; }

        public string  Title { get; set; }

        public Genre Genre { get; set; }

        public string Author { get; set; }
        
        public int QuantityAvailable { get; set; }
    }
}
