using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IDepartmentService
    {
        Task<IDataResult<IList<DepartmentDetailsDto>>> GetAll();
        Task<IDataResult<DepartmentDetailsDto>> GetByName(string departmentName);
        Task<IDataResult<DepartmentDetailsDto>> Add(DepartmentDetailsDto DepartmentDetailsDto);
        Task<IResult> Delete(int departmentId, string modifiedByName);
        Task<IResult> Update(DepartmentDetailsDto DepartmentDetailsDto);
    }
}
