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
        public Task<DepartmentDetailsDto> GetById(int departmentId);
        List<DepartmentDetailsDto> GetAllDepartments();
        public void Add(DepartmentDetailsDto departmentDetailsDto);
        void Delete(int departmentId, string modifiedByName);
        void Update(DepartmentUpdateDto departmentUpdateDto);
    }
}
