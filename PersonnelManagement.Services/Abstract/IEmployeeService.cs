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
        Task<IDataResult<EmployeeDetailsDto>> Add(); //içine muhtemelen model gönderilecek
        Task<IDataResult<List<EmployeeDetailsDto>>> GetAll();
    }
}
