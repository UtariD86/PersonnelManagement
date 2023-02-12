using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class AddEmployeeModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? NewEmployee { get; set; }

        [Required]
        public string? SelectedDepartment { get; set; }

        [Required]
        public string? SelectedPosition { get; set; }
    }
}
