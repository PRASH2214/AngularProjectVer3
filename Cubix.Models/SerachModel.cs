using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class SearchModel
    {
        public string SearchValue { get; set; }
        public int CurrentPage { get; set; }
        public int Skip { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int Status { get; set; }
        public long DoctorId { get; set; }

    }

    public class ReportSearchModel
    {
        public long HospitalId { get; set; }
        public long DoctorId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


    }
}
