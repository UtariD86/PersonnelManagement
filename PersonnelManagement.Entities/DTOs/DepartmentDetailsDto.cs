using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class DepartmentDetailsDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ModifiedByName { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
