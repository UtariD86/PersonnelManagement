using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class UpdatePositionModel
    {
        [Required]
        public int PositionId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? NewPosition { get; set; }

        [Required]
        public string? SelectedDepartment { get; set; }

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
