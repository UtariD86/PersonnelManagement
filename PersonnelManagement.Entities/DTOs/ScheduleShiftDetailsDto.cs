using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class ScheduleShiftDetailsDto
    {
        public int ScheduleShiftId { get; set; }
        public int ShiftId { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ModifiedByName { get; set; }
        public bool IsSpecial { get; set; }
        public string SpecialShiftType { get; set; }
        public TimeSpan? SpecialStartTime { get; set; }
        public TimeSpan? SpecialEndTime { get; set;}
        public string SpecialColor { get; set; }
        public DateTime? Enter { get; set; }
        public DateTime? Exit { get; set; }
    }
}
