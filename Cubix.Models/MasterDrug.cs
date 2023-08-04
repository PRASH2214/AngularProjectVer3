using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class MasterDrug
    {
        public long DrugId { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Name cannot be greater than 250")]
        public string DrugName { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Description cannot be greater than 500")]
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Modifieddate { get; set; }
        public long CreatedById { get; set; }
        public string Reason { get; set; }
    }

    public class MasterDrugUpload
    {
        public long DrugId { get; set; }
        [Required]
        public string DrugName { get; set; }
        [Required]
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Modifieddate { get; set; }
        public long CreatedById { get; set; }
        public string Reason { get; set; }
        public bool IsValid { get; set; }
    }
}
