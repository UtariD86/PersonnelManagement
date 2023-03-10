using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class UpdateEmployeeModel
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? NewEmployee { get; set; }

        [Required]
        public string? SelectedDepartment { get; set; }

        [Required]
        public string? SelectedPosition { get; set; }

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
