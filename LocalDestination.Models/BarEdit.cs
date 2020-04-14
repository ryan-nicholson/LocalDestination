using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDestination.Models
{
    public class BarEdit
    {
        [Required]
        public int BarId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public bool ServesFood { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Comment { get; set; }
        public int DestinationId { get; set; }
    }
}
