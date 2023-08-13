using PersonnelManagement.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class PositionModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Name { get; set; }

        public int? DepartmentId { get; set; }

        [Required]
        public string? DepartmentName { get; set; }

        public List<Department>? Departments { get; set; }

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
