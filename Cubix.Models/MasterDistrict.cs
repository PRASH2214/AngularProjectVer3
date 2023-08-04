using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterDistrict
    {
        public int Id { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? Status { get; set; }
    }
}
