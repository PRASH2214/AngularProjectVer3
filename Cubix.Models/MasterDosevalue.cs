using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterDosevalue
    {
        public int DosageValueId { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#*""]+$", ErrorMessage = "Invalid Dosage Value entry")]
        [MaxLength(250, ErrorMessage = "Value cannot be greater than 250")]
        public string DosageValue { get; set; }
        [Required]
        //  [RegularExpression(@"^[^<>.,?;:'()!~%\_@#/*""]+$", ErrorMessage = "Invalid Remarks entry")]
        [MaxLength(500, ErrorMessage = "Remarks cannot be greater than 500")]
        public string DosageRemarks { get; set; }
        public int Status { get; set; }
    }
}
