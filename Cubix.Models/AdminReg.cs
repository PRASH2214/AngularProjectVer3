using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class AdminReg
    {
        public long AdminId { get; set; }
        [Required]
        [MaxLength(99, ErrorMessage = "Name cannot be greater than 99")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string AdminName { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Address cannot be greater than 250")]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string Address { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Mobile cannot be greater than 10")]
        [MinLength(10, ErrorMessage = "Speciality cannot be less than 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
        public string AdminMobile { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedById { get; set; }
        [Required]
        [MaxLength(99, ErrorMessage = "Client Name cannot be greater than 99")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""]+$", ErrorMessage = "Invalid entry")]
        public string ClientName { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Web URL cannot be greater than 500")]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string WebUrl { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "MR URL cannot be greater than 500")]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string MrUrl { get; set; }
        public int UserTypeId { get; set; }
        public string Token { get; set; }
        [RegularExpression(@"[^<>]*", ErrorMessage = "Invalid entry")]
        public string ProfileImagePath { get; set; }
    }
}
