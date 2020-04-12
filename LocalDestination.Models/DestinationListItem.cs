using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDestination.Models
{
    public class DestinationListItem
    {
        public int DestinationId { get; set; }
        public string Name { get; set; }
        public bool IsUserOwned { get; set; }
    }
}
