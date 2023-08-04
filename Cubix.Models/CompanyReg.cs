using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class CompanyReg
    {
        public long CompanyId { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "CompanyBranch Name cannot be greater than 250")]
        public string CompanyName { get; set; }
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
        public string CompanyAddress { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(70, ErrorMessage = "License Number cannot be greater than 70")]
        public string CompanyLicenseNumber { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "SPOC Name cannot be greater than 99")]
        public string SpocName { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "SPOC Mobile must be numeric")]
        public string SpocMobile { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Admin Name cannot be greater than 99")]
        public string AdminName { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Mobile must be equall to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Admin Mobile must be numeric")]
        public string AdminMobile { get; set; }
        [Required]
        [MaxLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [MinLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Pin Code must be numeric")]
        public string PinCode { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Web Link Name cannot be greater than 500")]
        public string CompanyWebLink { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        public string Reason { get; set; }
    }



    public class CompanyRegUpload
    {
        public long CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        [Required]
        public string CompanyAddress { get; set; }
        [Required]
        public string CompanyLicenseNumber { get; set; }
        [Required]
        public string SpocName { get; set; }
        [Required]
        public string SpocMobile { get; set; }
        [Required]
        public string AdminName { get; set; }
        [Required]
        public string AdminMobile { get; set; }
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string CompanyWebLink { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        [Required]
        public string StateName { get; set; }
        [Required]
        public string DistrictName { get; set; }
        [Required]
        public string CityName { get; set; }
        public bool IsValid { get; set; }
        public string Reason { get; set; }

    }


}
