using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;
using zurafworks.Shared.Utilities.Results.ComplexTypes;
using zurafworks.Shared.Utilities.Results.Concrete;

namespace PersonnelManagement.Services.Concrete
{
    public class PositionManager : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        public PositionManager(IPositionRepository positionRepository)
        {
            _positionRepository= positionRepository;
        }
        public async Task<IDataResult<List<PositionDetailsDto>>> GetAll()
        {
            var positions = _positionRepository.GetAllPositions();
            if (positions.Count > -1)
            {
                return new DataResult<List<PositionDetailsDto>>(ResultStatus.Success, positions);
            }
            return new DataResult<List<PositionDetailsDto>>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
        }
    }
}
