using System.ComponentModel.DataAnnotations;
namespace BibliotekBoklusen.Shared
{
    public class Seminarium
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required field")]
        public DateTime SeminarDate { get; set; }
        [Required(ErrorMessage = "Required field")]
        public DateTime SeminarTime { get; set; }
    }
}
