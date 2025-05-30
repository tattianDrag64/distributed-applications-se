using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotekBoklusen.Shared
{
    public class Creator
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }
        public List<Product>? Products { get; set; }

    }
}
