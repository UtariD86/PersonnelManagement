using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class ShiftDetailsDto
    {
        public int  ShiftId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public int ShiftTypeId { get; set; }
        public string? ShiftTypeName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Color { get; set; }
        public string ModifiedByName { get; set; }
    }
}
