using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterDuration
    {
        public int DurationId { get; set; }
        [MaxLength(250, ErrorMessage = "Duration cannot be greater than 250")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string Duration { get; set; }
        [MaxLength(500, ErrorMessage = "Description cannot be greater than 500")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
