using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IDataResult<EmployeeDetailsDto>> Add(EmployeeDetailsDto employeeDetailsDto); //içine createdbyname i de gönder giriş yapan kullanıcıya göre
        Task<IDataResult<List<EmployeeDetailsDto>>> GetAll();
        Task<IResult> Delete(int employeeId, string modifiedByName);
    }
}
