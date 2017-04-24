using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni_LibraryManagement.Data.Entities
{
    public class BookIssue : BaseEntity
    {
        public DateTime? Issued { get; set; }
        
        public DateTime? Returned { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime? DeadLine { get; set; }

        public int QuantityIssued { get; set; }
        
        public int QuantityReturned { get; set; }
    }
}
