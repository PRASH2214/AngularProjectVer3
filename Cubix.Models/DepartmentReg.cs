using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
    public class DepartmentReg
    {
        public long DepartmentId { get; set; }
        [Required]
        public long HospitalId { get; set; }
        [Required]
        public long BranchId { get; set; }
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        [MaxLength(250, ErrorMessage = "Department Name cannot be greater than 250")]
        public string DepartmentName { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Contact Mobile must be equal to 10")]
        [MinLength(10, ErrorMessage = "Contact Mobile must be equal to 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Mobile must be numeric")]
        public string DepartmentContactMobile { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        [MaxLength(500, ErrorMessage = "Department link cannot be greater than 500")]
        public string HospitalDepartmentLink { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedByID { get; set; }
        //    [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        public decimal Amount { get; set; }
        public int IsRefundAllowed { get; set; }
        public string Reason { get; set; }
    }


    public class DepartmentRegUpload
    {
        public long DepartmentId { get; set; }
        public long HospitalId { get; set; }
        public long BranchId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string DepartmentContactMobile { get; set; }
        [Required]
        public string HospitalDepartmentLink { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedByID { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public int IsRefundAllowed { get; set; }
        public string Reason { get; set; }
        [Required]
        public string HospitalName { get; set; }
        [Required]
        public string BranchName { get; set; }
        public bool IsValid { get; set; }
    }



}
