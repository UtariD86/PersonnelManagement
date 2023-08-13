using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class PositionDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string ModifiedByName { get; set; }
    }
}
