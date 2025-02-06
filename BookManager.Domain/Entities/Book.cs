using BookManager.Domain.Entities.Shared;

namespace BookManager.Domain.Entities
{
    public class Book : BaseEntity<Guid>,IBaseAuditEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //Relation
        public Guid AuthorId { get; set; }

        public Author Author { get; set; } = null!;
       
    }
}
