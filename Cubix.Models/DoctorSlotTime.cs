using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class DoctorSlotTime
    {
        public long DoctorSlotTimeId { get; set; }
        public long MasterSlotId { get; set; }
        public long DoctorId { get; set; }
        public int DayId { get; set; }
        public int Status { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string DayName { get; set; }
        public long SlotDepartmentTimeId { get; set; }
        public int ForUserTypeId { get; set; }
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan SlotFromTime { get; set; }
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan SlotEndTime { get; set; }
        public DateTime SlotDate { get; set; }
        public int NoOfPatientsAllowed { get; set; }
        public int NoOfPatientsBooked { get; set; }
        public int SlotTimePerPatient { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedById { get; set; }
        public DateTime ModifiedDate { get; set; }
    }


    public class DoctorSlotRequest
    {
        public long DepartmentId { get; set; }
        public long DoctorId { get; set; }
        public int DayId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string DayName { get; set; }
        public DateTime SlotDate { get; set; }
        public int ForUserTypeId { get; set; }
    }
}
