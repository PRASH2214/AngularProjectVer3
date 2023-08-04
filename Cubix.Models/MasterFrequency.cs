using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterFrequency
    {
        public long FrequencyTakenId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FrequencyTaken { get; set; }
        public int Status { get; set; }
    }
}
