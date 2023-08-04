using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class ReportPatientResultModel
    {
        public string HospitalName { get; set; }
        public string BranchName { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public int AppointmentType { get; set; }
        public DateTime MonthYear { get; set; }
        public int TotalPatientAppointments { get; set; }
        public int TotalPatientConsulted { get; set; }
        public float TotalFeesAmount { get; set; }
        public float TotalRefundAmount { get; set; }
        public long CreatedBy { get; set; }


    }
    public class ReportMRResultModel
    {
        public string HospitalName { get; set; }
        public string BranchName { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public int AppointmentType { get; set; }
        public DateTime MonthYear { get; set; }
        public int TotalMRAppointments { get; set; }
        public int TotalMRConsulted { get; set; }
        public float TotalFeesAmount { get; set; }
        public float TotalRefundAmount { get; set; }
        public long CreatedBy { get; set; }


    }
}
