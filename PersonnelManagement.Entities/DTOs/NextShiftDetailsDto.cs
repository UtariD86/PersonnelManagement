using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class NextShiftDetailsDto
    {
        public int ScheduleShiftId { get; set; }
        public int ShiftId { get; set; }
        public string ShiftTypeName { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ModifiedByName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Color { get; set; }
        public DateTime? Enter { get; set; }
        public DateTime? Exit { get; set; }
    }
}
