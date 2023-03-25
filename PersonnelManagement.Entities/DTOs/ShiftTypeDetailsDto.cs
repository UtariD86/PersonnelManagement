using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class ShiftTypeDetailsDto
    {
        public int ShiftTypeId { get; set; }
        public string? ShiftTypeName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Color { get; set; }//vardiyayı temsil eden ve takvimde görünecek renk
        public string ModifiedByName { get; set; }
    }
}
