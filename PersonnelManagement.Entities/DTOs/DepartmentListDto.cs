using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Results.Abstract;

namespace PersonnelManagement.Entities.DTOs
{
    public class DepartmentListDto : DtoGetBase
    {
            public IList<Department> Departments { get; set; }

    }
}
