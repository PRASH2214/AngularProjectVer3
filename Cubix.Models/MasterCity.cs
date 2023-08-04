using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class MasterCity
    {
        public int Id { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int Status { get; set; }
    }
}
