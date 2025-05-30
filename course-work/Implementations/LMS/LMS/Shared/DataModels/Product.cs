using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotekBoklusen.Shared
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string Title { get; set; } = String.Empty;

        [Required(ErrorMessage = "Required Field")]
        [Column(TypeName = "datetime2")]
        public DateTime Published { get; set; }

        public ProductType Type { get; set; }

        public int NumberOfCopiesOwned { get; set; }
        public List<Category>? Category { get; set; } = new();
        public List<Creator>? Creators { get; set; } = new();
    }
}
