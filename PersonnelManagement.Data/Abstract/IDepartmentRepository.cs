using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Abstract;

namespace PersonnelManagement.Data.Abstract
{
    public interface IDepartmentRepository : IEntityRepository<Department>
    {
        Task<DepartmentDetailsDto> GetByName(string departmentName);
        List<DepartmentDetailsDto> GetAllDepartments();
    }
}
