using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cubix.Models
{
  public class FileUpload
    {
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ImagePath { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileName { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileFlag { get; set; }
        public long Id { get; set; }
    }

    public class BranchFileUpload
    {
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ImagePath { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileName { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileFlag { get; set; }
        public long Id { get; set; }
        public long HospitalId { get; set; }
    }

    public class DepartmentFileUpload
    {
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ImagePath { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileName { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileFlag { get; set; }
        public long Id { get; set; }
        public long HospitalId { get; set; }
        public long BranchId { get; set; }
    }

    public class DoctorFileUpload
    {
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ImagePath { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileName { get; set; }
        [Required]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string FileFlag { get; set; }
        public long Id { get; set; }
        public long HospitalId { get; set; }
        public long BranchId { get; set; }
        public long DepartmentId { get; set; }
    }
}
