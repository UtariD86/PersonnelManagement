using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IShiftService
    {
        Task<IDataResult<ShiftDetailsDto>> Add(ShiftDetailsDto shiftDetailsDto);
        Task<IDataResult<List<ShiftDetailsDto>>> GetAll();
        Task<IResult> Delete(/*int employeeId, string modifiedByName*/ShiftDetailsDto shiftDetailsDto);
        Task<IResult> Update(ShiftDetailsDto shiftDetailsDto);
    }
}
