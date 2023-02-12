using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class PositionDetailsDto
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
    }
}
