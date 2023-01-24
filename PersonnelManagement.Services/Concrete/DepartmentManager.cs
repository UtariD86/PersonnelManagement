using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;
using zurafworks.Shared.Utilities.Results.Concrete;
using zurafworks.Shared.Utilities.Results.ComplexTypes;

namespace PersonnelManagement.Services.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        IDepartmentRepository _departmentRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository= departmentRepository;
        }

        public async Task<IDataResult<List<DepartmentDetailsDto>>> GetAll()
        {
            var departments = _departmentRepository.GetAllDepartments();
            if (departments.Count >- 1)
            {
                return new DataResult<List<DepartmentDetailsDto>>(ResultStatus.Success, departments);
            }
            return new DataResult<List<DepartmentDetailsDto>>(ResultStatus.Error, "Hiç Departman bulunamadı", null);
        }
    }
}
