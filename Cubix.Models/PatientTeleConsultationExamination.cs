using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class PatientTeleConsultationExamination
    {
        public long ConsultationExamId { get; set; }
        public long PatientTeleConsultationId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "General Examination cannot be greater than 250")]
        public string GeneralExamination { get; set; }
        public int IsSmoker { get; set; }
        public int IsDiabetic { get; set; }
        public int IsAlcoholic { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Additional Medicine cannot be greater than 250")]
        public string AdditionalMedicine { get; set; }
        public DateTime CreatedDate { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(50, ErrorMessage = "Reference Number cannot be greater than 50")]
        public string ConsultationReferenceNumber { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
    }
}
