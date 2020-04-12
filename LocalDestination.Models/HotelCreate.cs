using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDestination.Models
{
    public class HotelCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public int DestinationId { get; set; }
    }
}
