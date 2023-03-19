using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class Position : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        //public virtual ICollection<Employee> Employees { get; set; }
        //her pozisyonun çalışanları sıralanabilir repositoryde join yapılarak çözülecek
    }
}
