using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class DoctorReg
    {
        public long DoctorId { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(45, ErrorMessage = "First Name cannot be greater than 45")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(45, ErrorMessage = "Middle Name cannot be greater than 45")]
        public string MiddleName { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(45, ErrorMessage = "Last Name cannot be greater than 45")]
        public string LastName { get; set; }
        [Required]
        public long HospitalId { get; set; }
        public int CountryId { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Address cannot be greater than 250")]
        public string DoctorAddress { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email Address is not valid")]
        [MaxLength(250, ErrorMessage = "Email Address cannot be greater than 250")]
        public string EmailAddress { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(70, ErrorMessage = "License Number cannot be greater than 70")]
        public string DoctorLicenseNumber { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Mobile must be equal to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
        public string Mobile { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string MedicalLicenseImage { get; set; }
        [Required]
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        [Required]
        [MaxLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [MinLength(6, ErrorMessage = "Pin Code must be equal to 6")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Pin Code must be numeric")]
        public string PinCode { get; set; }
        [Required]
        public long SpecialityId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ImagePath { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ProfileImagePath { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileName { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileFlag { get; set; }
        [Required]
        public long BranchId { get; set; }
        [Required]
        public long DepartmentId { get; set; }
        public string Token { get; set; }
        public string Reason { get; set; }
    }


    public class DoctorRegUpload
    {
        [Required]
        public string strGender { get; set; }

        public long DoctorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public long HospitalId { get; set; }
        public int CountryId { get; set; }
        public int GenderId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        [Required]
        public string DoctorAddress { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string DoctorLicenseNumber { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string MedicalLicenseImage { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        [Required]
        public string PinCode { get; set; }
        public long SpecialityId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        public string ImagePath { get; set; }
        public string ProfileImagePath { get; set; }
        public string FileName { get; set; }
        public string FileFlag { get; set; }
        public long BranchId { get; set; }
        public long DepartmentId { get; set; }
        [Required]
        public string StateName { get; set; }
        [Required]
        public string DistrictName { get; set; }
        [Required]
        public string CityName { get; set; }
        public string Reason { get; set; }
        [Required]
        public string HospitalName { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string SpecialityName { get; set; }
        [Required]
        public string strDOB { get; set; }

        public bool IsValid { get; set; }
    }





}
