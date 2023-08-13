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
    public interface IShiftTypeService
    {
        Task<IDataResult<IList<ShiftType>>> GetAll();
        Task<IDataResult<ShiftType>> GetByName(ShiftType shiftType);
        Task<IDataResult<ShiftType>> Add(ShiftType shiftType);
        Task<IResult> Delete(ShiftType shiftType);
        Task<IResult> Update(ShiftType shiftType);
        Task<IDataResult<ShiftType>> GetById(int id);
        Task<IResult> BulkInsert(IList<ShiftType> shiftTypes);
        MemoryStream CreateExcel();
    }
}
