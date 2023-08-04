using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterMedicine
    {
        public long MedicineId { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Medicine Name cannot be greater than 250")]
        public string MedicineName { get; set; }
        public int Status { get; set; }
        [Required]
        public long CompanyId { get; set; }
        [Required]
        public long DrugId { get; set; }
        [Required]
        public long DrugType { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Description Name cannot be greater than 500")]
        public string Description { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Company  Name cannot be greater than 250")]
        public string CompanyName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Reason { get; set; }
    }
    public class MasterMedicineUpload
    {
        public long MedicineId { get; set; }
        [Required]
        public string MedicineName { get; set; }
        public int Status { get; set; }
        public long CompanyId { get; set; }
        public long DrugId { get; set; }
        public long DrugType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string DrugName { get; set; }
        public string DrugTypeName { get; set; }
        public string Reason { get; set; }
        public bool IsValid { get; set; }
    }
}
