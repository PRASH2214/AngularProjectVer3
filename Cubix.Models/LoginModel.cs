using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class LoginModel
    {

        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [DisplayName("Mobile Number")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string OTP { get; set; }
    }

    public class PatientLoginModel
    {

        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ConsultationReferenceNumber { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string OTP { get; set; }
        public int RelationId { get; set; }
    }
}
