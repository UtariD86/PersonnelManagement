using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class AddPositionModel
    {
        [Required]
        public string SelectedDepartment { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string NewPosition { get; set; }
    }
}
