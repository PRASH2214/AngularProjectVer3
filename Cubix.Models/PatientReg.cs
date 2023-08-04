using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class ResultPatientReg
    {
        public long PatientId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string PatientReferenceNumber { get; set; }

        public long PatientTeleConsultationId { get; set; }
    }
    public class PatientReg
    {
        public long PatientId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string PatientReferenceNumber { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(45, ErrorMessage = "First Name cannot be greater than 45")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(45, ErrorMessage = "Last Name cannot be greater than 45")]
        public string LastName { get; set; }
        public int CountryId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Address cannot be greater than 250")]
        public string PatientAddress { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
        public string Mobile { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        [Required]
        [MaxLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [MinLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Pin Code must be numeric")]
        public string PinCode { get; set; }
        public int? Status { get; set; }
        public int? GenderId { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email Address is not valid")]
        [MaxLength(250, ErrorMessage = "Email Address cannot be greater than 250")]
        public string EmailAddress { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ProfileImagePath { get; set; }
        public int? RelationId { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(45, ErrorMessage = "Relation Name cannot be greater than 45")]
        public string RelationName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string OTP { get; set; }
        public string Token { get; set; }
    }


    public class SuperPatientRegistrationModel
    {
        public PatientReg PatientReg { get; set; }
        public PatientTeleConsultationReg PatientTeleConsultationReg { get; set; }
        public List<PatientDocumentReg> lstPatientDocumentReg { get; set; }

    }
}
