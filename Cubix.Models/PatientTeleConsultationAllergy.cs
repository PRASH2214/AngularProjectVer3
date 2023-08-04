using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class PatientTeleConsultationAllergy
    {
        public long ConsultationAllergyId { get; set; }
        public long PatientTeleConsultationId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Name cannot be greater than 250")]
        public string Name { get; set; }
        public bool IsStill { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Notes cannot be greater than 500")]
        public string Notes { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Severity Type cannot be greater than 99")]
        public string SeverityType { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Duration Type cannot be greater than 99")]
        public string Duration { get; set; }
        public DateTime CreatedDate { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(50, ErrorMessage = "Reference Number cannot be greater than 50")]
        public string ConsultationReferenceNumber { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
    }
}
