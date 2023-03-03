using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class DeleteDepartmentModel
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
