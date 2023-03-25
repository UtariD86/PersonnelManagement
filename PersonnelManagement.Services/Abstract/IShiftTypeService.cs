using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IShiftTypeService
    {
        Task<IDataResult<List<ShiftTypeDetailsDto>>> GetAll();
        Task<IDataResult<ShiftTypeDetailsDto>> GetByName(string shiftTypeName);
        Task<IDataResult<ShiftTypeDetailsDto>> Add(ShiftTypeDetailsDto shiftTypeDetailsDto);
        Task<IResult> Delete(int shiftTypeId, string modifiedByName);
        Task<IResult> Update(ShiftTypeDetailsDto shiftTypeDetailsDto);
    }
}
