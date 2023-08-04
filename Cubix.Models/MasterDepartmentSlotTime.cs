using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class MasterDepartmentSlotTime
    {
        public long SlotDepartmentTimeId { get; set; }
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan SlotFromTime { get; set; }
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan SlotEndTime { get; set; }
        public long DepartmentId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        public int? Type { get; set; }
        public int? MasterSlotId { get; set; }
        public int? DayId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string DayName { get; set; }
        public bool IsActive { get; set; }

    }

    public class DepartmentSlotRequest
    {
        public long DepartmentId { get; set; }
        public int DayId { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string DayName { get; set; }
    }
}
