using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IDataResult<IList<Employee>>> GetAll();
        Task<IDataResult<IList<EmployeeDetailsDto>>> GetAllWithRelations();
        Task<IDataResult<Employee>> Add(Employee employee);
        Task<IResult> Delete(Employee employee);
        Task<IDataResult<Employee>> GetById(int id);
        Task<IResult> Update(Employee employee);
        MemoryStream ExportToExcel();
    }
}
