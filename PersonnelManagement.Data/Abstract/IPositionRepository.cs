using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Abstract;

namespace PersonnelManagement.Data.Abstract
{
    public interface IPositionRepository : IEntityRepository<Position>
    {
        Task<PositionDetailsDto> GetByName(string positionName);
        List<PositionDetailsDto> GetAllPositions();
        void Add(PositionDetailsDto positionDetailsDto);
        public Task<PositionDetailsDto> GetById(int positionId);
        void Delete(int positionId, string modifiedByName);
    }
}
