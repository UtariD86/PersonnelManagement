using Microsoft.Build.Framework;
using Newtonsoft.Json.Serialization;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Mvc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace PersonnelManagement.Mvc.Areas.Admin.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string UserName { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? CurrentPassword { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^\+90 \(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Invalid Phone Number.")]
        public string? PhoneNumber { get; set; }

        public int? RoleId { get; set; }

        public List<Role>? Roles { get; set; }

        public string? Dial_Code { get; set; }

        public IFormFile? Picture { get; set; }

        public string? PictureUrl { get; set; }
        
        public string? PictureName { get; set; }

        public List<CountryCodeModel>? CountryCodes { get; set; }

        public int? EmployeeId { get; set; }

        public EmployeeModel? Employee { get; set; }
    }
}
