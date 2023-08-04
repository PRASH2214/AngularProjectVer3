using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterRegion
    {
        public int RegionTypeId { get; set; }
        public string RegionName { get; set; }
        public int? Status { get; set; }
    }
}
