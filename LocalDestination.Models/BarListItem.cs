using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDestination.Models
{
    public class BarListItem
    {
        public int BarId { get; set; }

        public string Name { get; set; }
        public bool IsUserOwned { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        public int DestinationId { get; set; }

        [Display(Name = "Destination")]
        public string DestinationName { get; set; }
    }
}
