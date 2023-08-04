using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class PatientTeleConsultationMedicine
    {
        public long ConsultationMedicineId { get; set; }
        public long PatientTeleConsultationId { get; set; }
        public long? MedicineId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(200, ErrorMessage = "Medicine Name cannot be greater than 200")]
        public string MedicineName { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Frequency cannot be greater than 99")]
        public string Frequency { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Dosage cannot be greater than 99")]
        public string Dosage { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(45, ErrorMessage = "Dosage Type cannot be greater than 45")]
        public string DosageType { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Duration cannot be greater than 99")]
        public string Duration { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Notes cannot be greater than 500")]
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(50, ErrorMessage = "Reference Number cannot be greater than 50")]
        public string ConsultationReferenceNumber { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
    }

}
