using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ModifiedByName { get; set; }
    }
}
