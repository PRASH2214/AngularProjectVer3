using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class TokenModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string LoginId { get; set; }
        public string EmailAddress { get; set; }
        public string UserTypeId { get; set; }
        public string CreatedById { get; set; }
        public string DepartmentId { get; set; }
    }


    public class TokenCacheModel
    {
        public long LoginId { get; set; }
        public string Token { get; set; }
        public int UserTypeId { get; set; }

    }
}
