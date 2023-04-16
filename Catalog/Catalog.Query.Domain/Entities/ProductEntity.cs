using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Query.Domain.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool Active { get; set; } = false;
        public int Stock { get; set; }
        public DateTime ModificationDate { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual ProductCategoryEntity Category { get; set; }
    }
}
