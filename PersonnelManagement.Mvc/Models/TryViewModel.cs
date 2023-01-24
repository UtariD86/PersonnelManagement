using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class TryViewModel
    {
        //ekleme fonksiyonu için kullanılacak
        //max min kontrolü araştırılacak
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? NewEmployee { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string? Department { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string? Position { get; set; }
    }
}
