using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class Department : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Position>? Positions { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
        //her departmana özel posisyonlar vardır. Örneğin Yazılım dept. => a)Proje Yöneticisi b)Geliştirici c)Devops Sorumlusu gibi
        // her departmandaki çalışanlar pozisyonlarına göre sıralanabilir
    }
}
