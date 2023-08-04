using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterSeverityTypeData
    {
        public int SeverityTypeId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string Name { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}
