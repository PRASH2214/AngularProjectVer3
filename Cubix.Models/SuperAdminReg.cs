using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class SuperAdminReg
    {
        public long SuperAdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminMobile { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        public string ProfileImagePath { get; set; }
        public string Token { get; set; }
    }
}
