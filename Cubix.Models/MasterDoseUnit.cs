using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterDoseUnit
    {
        public int DosageUnitId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string DosageUnit { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
