using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterAllergySeverity
    {
        public int AllergySeverityId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string AllergySeveritySnId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string AllergySeverityName { get; set; }
        public int? Status { get; set; }
    }
}
