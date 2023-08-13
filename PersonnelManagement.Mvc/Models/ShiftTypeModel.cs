using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class ShiftTypeModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

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

        [Required]
        public string? ModifiedByName { get; set; }
    }
}
