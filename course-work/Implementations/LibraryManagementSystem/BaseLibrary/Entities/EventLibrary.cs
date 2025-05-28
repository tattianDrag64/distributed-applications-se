using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class EventLibrary
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }

        public int OrganizerId { get; set; }
        public User Organizer { get; set; }
    }
}
