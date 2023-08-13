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
    public interface IDepartmentService
    {
        Task<IDataResult<IList<Department>>> GetAll();
        Task<IDataResult<Department>> Add(Department department);
        Task<IResult> Delete(Department department);
        Task<IResult> Update(Department department);
        Task<IDataResult<Department>> GetById(int id);
    }
}
