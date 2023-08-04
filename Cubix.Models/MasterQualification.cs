using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterQualification
    {
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
        public int? Status { get; set; }
    }
}
