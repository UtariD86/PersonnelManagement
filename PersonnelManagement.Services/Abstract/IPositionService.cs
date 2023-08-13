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
    public interface IPositionService
    {
        Task<IDataResult<IList<Position>>> GetAll();
        Task<IDataResult<IList<PositionDetailsDto>>> GetAllWithRelations();
        Task<IDataResult<Position>> Add(Position position);
        Task<IResult> Delete(Position position);
        Task<IDataResult<Position>> GetById(int id);
        Task<IResult> Update(Position position);
    }
}
