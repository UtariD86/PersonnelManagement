using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class ShiftType : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public string? Name { get; set; }
        public TimeSpan? StartTime { get; set; }//vardiyanın başlangıç ve bitiş saatleri
        public TimeSpan? EndTime { get; set; }
        public string Color { get; set; }//vardiyayı temsil eden ve takvimde görünecek renk
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
