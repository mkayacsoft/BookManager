using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Domain.Entities.Shared;

namespace BookManager.Domain.Entities
{
    public class Author:BaseEntity<Guid>,IBaseAuditEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Relation
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
