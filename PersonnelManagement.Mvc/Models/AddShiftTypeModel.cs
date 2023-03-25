using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class AddShiftTypeModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string InputtedShiftType { get; set; }

        //[Required]
        //[MinLength(5)]
        //[MaxLength(50)]
        //public TimeSpan? StartTime { get; set; }

        //[Required]
        //[MinLength(5)]
        //[MaxLength(50)]
        //public TimeSpan? EndTime { get; set; }

        [Required]
        [Display(Name = "Başlangıç Saati")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan? StartTime { get; set; }

        [Required]
        [Display(Name = "Bitiş Saati")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan? EndTime { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
