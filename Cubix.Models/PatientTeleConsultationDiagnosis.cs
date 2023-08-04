using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class PatientTeleConsultationDiagnosis
    {
        public long ConsultationDiagnosisId { get; set; }
        public long PatientTeleConsultationId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Name cannot be greater than 99")]
        public string Name { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Code cannot be greater than 99")]
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(50, ErrorMessage = "Reference Number cannot be greater than 50")]
        public string ConsultationReferenceNumber { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
    }
}
