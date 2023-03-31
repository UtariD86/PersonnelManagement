using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class AddScheduleShiftModel
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ShiftTypeId { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public string ModifiedByName { get; set; }

        [Required]
        public bool IsSpecial { get; set; }

        //for special shifttype
        [MinLength(5)]
        [MaxLength(50)]
        public string SpecialShiftType { get; set; }

        [Display(Name = "Başlangıç Saati")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan? SpecialStartTime { get; set; }

        [Display(Name = "Bitiş Saati")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan? SpecialEndTime { get; set; }

        public string SpecialColor { get; set; }
    }
}
