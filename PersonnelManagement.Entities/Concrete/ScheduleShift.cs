using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class ScheduleShift : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
        //public int ScheduleId { get; set; }
        //public Schedule Schedule { get; set; }
        public DateTime? StartDate { get; set; } //gün tarih
        public DateTime? EndDate { get; set; } //gün tarih
        //public virtual ICollection<ScheduleShift> ScheduleShifts { get; set; }
    }
}
