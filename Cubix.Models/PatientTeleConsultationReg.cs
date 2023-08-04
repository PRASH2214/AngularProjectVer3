using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class PatientTeleConsultationReg
    {
        public long PatientTeleConsultationId { get; set; }
        public long PatientId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string PatientReferenceNumber { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ConsultationReferenceNumber { get; set; }
        public long HospitalId { get; set; }
        public long BranchId { get; set; }
        public long DepartmentId { get; set; }
        public long DoctorId { get; set; }
        public long DoctorSlotTimeId { get; set; }
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan SlotFromTime { get; set; }
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan SlotEndTime { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaymentAmmount { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
        public string Mobile { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Concern Name cannot be greater than 500")]
        public string Concern { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int ConsultationsStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Token { get; set; }
        public string PaymentLink { get; set; }
        public string PaymentReferenceId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(50, ErrorMessage = "Payment Mode cannot be greater than 50")]
        public string PaymentMode { get; set; }
        [MaxLength(500, ErrorMessage = "Advice cannot be greater than 500")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string Advice { get; set; }
    }

    public class PatientTeleConsultationDetail
    {
        public long PatientTeleConsultationId { get; set; }
        public string PatientReferenceNumber { get; set; }
        public long PatientId { get; set; }
        public string ConsultationReferenceNumber { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string LastName { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string PatientAddress { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
        public int GenderId { get; set; }
        public int Age { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ProfileImagePath { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string HospitalName { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string BranchName { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string DepartmentName { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string DoctorName { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string Concern { get; set; }
        public int Type { get; set; }
        //[RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string PaymentMode { get; set; }
        // [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string Advice { get; set; }
        public DateTime CompletedDateTime { get; set; }

    }

    public class SuperPatientTeleConsultation
    {
        public PatientTeleConsultationDetail PatientTeleConsultationDetail { get; set; }
        public List<PatientDocumentReg> PatientDocumentReg { get; set; }
        public List<PatientTeleConsultationAllergy> PatientTeleConsultationAllergy { get; set; }
        public PatientTeleConsultationExamination PatientTeleConsultationExamination { get; set; }
        public List<PatientTeleConsultationDiagnosis> PatientTeleConsultationDiagnosis { get; set; }
        public List<PatientTeleConsultationMedicine> PatientTeleConsultationMedicine { get; set; }
        public SuperPatientTeleConsultation()
        {
            PatientDocumentReg = new List<PatientDocumentReg>();
            PatientTeleConsultationAllergy = new List<PatientTeleConsultationAllergy>();
            PatientTeleConsultationExamination = new PatientTeleConsultationExamination();
            PatientTeleConsultationDiagnosis = new List<PatientTeleConsultationDiagnosis>();
            PatientTeleConsultationMedicine = new List<PatientTeleConsultationMedicine>();
        }
    }
}
