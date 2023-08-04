using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class BranchReg
    {
        public long BranchId { get; set; }
        [Required]
        public long HospitalId { get; set; }
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
        public string BranchHospitalAddress { get; set; }
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [Required]
        [MaxLength(70, ErrorMessage = "License cannot be greater than 70")]
        public string BranchHospitalLicenseNumber { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(99, ErrorMessage = "Contact Name cannot be greater than 99")]
        public string ContactName { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Mobile must be numeric")]
        public string ContactMobile { get; set; }
        [Required]
        [MaxLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [MinLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Pin Code must be numeric")]
        public string PinCode { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Hospital Link cannot be greater than 500")]
        public string BranchHospitalLink { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Branch Name cannot be greater than 250")]
        public string BranchName { get; set; }
        public string Reason { get; set; }
    }


    public class BranchRegUpload
    {
        public long BranchId { get; set; }
        public long HospitalId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        [Required]
        public string BranchHospitalAddress { get; set; }
        [Required]
        public string BranchHospitalLicenseNumber { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactMobile { get; set; }
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string BranchHospitalLink { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string StateName { get; set; }
        [Required]
        public string DistrictName { get; set; }
        [Required]
        public string CityName { get; set; }
        public string Reason { get; set; }
        public string HospitalName { get; set; }  
        public bool IsValid { get; set; }
    }
}
