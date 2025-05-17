using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class EventDTO
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string Organizer { get; set; }
    }
}
