using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterState
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string State_or_UT { get; set; }
        public int? StateCode { get; set; }
        public string PincodeInitial { get; set; }
        public bool? IsMappingActive { get; set; }
        public int? Status { get; set; }
    }
}
