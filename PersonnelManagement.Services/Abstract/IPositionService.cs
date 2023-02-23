using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IPositionService
    {
        Task<IDataResult<List<PositionDetailsDto>>> GetAll();
        Task<IDataResult<PositionDetailsDto>> Add(PositionDetailsDto positionDetailsDto);
    }
}
