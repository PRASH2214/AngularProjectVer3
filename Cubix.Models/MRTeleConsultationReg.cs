using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class MRResponseTeleConsultationReg
    {
        public long MRTeleConsultationId { get; set; }
        public long MRId { get; set; }
        public string Response { get; set; }
    }
    public class MRTeleConsultationReg
    {
        public long MRTeleConsultationId { get; set; }
        public long MRId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string MRReferenceNumber { get; set; }
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

        [Required]
        [MaxLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
        public string Mobile { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Concern Name cannot be greater than 500")]
        public string Subject { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int ConsultationsStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Response { get; set; }
    }

}
