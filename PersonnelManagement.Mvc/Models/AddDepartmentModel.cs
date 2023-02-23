using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class AddDepartmentModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string NewDepartment { get; set; }
    }
}
