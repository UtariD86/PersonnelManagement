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
    public interface IShiftService
    {
        Task<IDataResult<Shift>> Add(Shift shift);
        Task<IDataResult<IList<Shift>>> GetAll();
        Task<IResult> Delete(Shift shift);
        Task<IResult> Update(Shift shift);
        Task<IDataResult<Shift>> GetById(int id);

    }
}
