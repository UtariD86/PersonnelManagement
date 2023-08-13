using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Picture { get; set; }

        public bool IsDeleted { get; set; }

        public int? EmployeeId { get; set; }
    }
}
