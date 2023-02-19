using Microsoft.Build.Framework;

namespace PersonnelManagement.Mvc.Models
{
    public class DeleteEmployeeModel
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
