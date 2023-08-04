using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterAllergyDuration
    {
        public int AllergyDurationId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string AllergyDuration { get; set; }
        public int? Status { get; set; }
    }
}
