using Microsoft.AspNetCore.Mvc.Infrastructure;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;
using zurafworks.Shared.Utilities.Results.ComplexTypes;
using zurafworks.Shared.Utilities.Results.Concrete;

namespace PersonnelManagement.Services.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<IDataResult<EmployeeDetailsDto>> Add()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<EmployeeDetailsDto>>> GetAll()
        {
            var employees = _employeeRepository.GetAllEmployees();
            if (employees.Count > -1)
            {
                return new DataResult<List<EmployeeDetailsDto>>(ResultStatus.Success, employees);
            }
            return new DataResult<List<EmployeeDetailsDto>>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
            
        }
    }
}
