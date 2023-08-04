using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class UserLogin
    {
        public long LoginId { get; set; }
        public int UserTypeId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string UserMobile { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string Otp { get; set; }
        public int Status { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string UserEmail { get; set; }
        public long ReferenceId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string UserName { get; set; }
    }
}
