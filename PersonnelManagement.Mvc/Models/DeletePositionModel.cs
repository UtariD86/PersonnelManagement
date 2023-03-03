using Microsoft.Build.Framework;

namespace PersonnelManagement.Mvc.Models
{
    public class DeletePositionModel
    {
        [Required]
        public int PositionId { get; set; }

        [Required]
        public string ModifiedByName { get; set; }
    }
}
