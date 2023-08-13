using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class Shift : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ShiftTypeId { get; set; }
        public ShiftType ShiftType { get; set; }
        public DateTime? Enter { get; set; } //giriş saati
        public DateTime? Exit { get; set; } //çıkış saati
        public virtual ICollection<ScheduleShift> ScheduleShifts { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
