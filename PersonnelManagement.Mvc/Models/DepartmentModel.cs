using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class DepartmentModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
