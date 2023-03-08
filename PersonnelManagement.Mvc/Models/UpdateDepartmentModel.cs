using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class UpdateDepartmentModel
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? NewDepartment { get; set; }

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
