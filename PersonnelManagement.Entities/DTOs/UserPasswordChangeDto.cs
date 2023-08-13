using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mootableProject.Shared.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şu anki şifreniz")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DisplayName("Yeni Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Yeni şifrenizin tekrarı")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Girmiş olduğunuz yeni şifreniz ile yeni şifrenizin tekrarı birbiriyle uyuşmalıdır.")]
        public string RepeatPassword { get; set; }
    }
}
