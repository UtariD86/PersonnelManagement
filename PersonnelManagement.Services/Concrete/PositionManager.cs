using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.Concrete;
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
        //private readonly IPositionRepository _positionRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PositionManager(/*IPositionRepository positionRepository, IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            //_positionRepository = positionRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork= unitOfWork;
        }

        public async Task<IDataResult<PositionDetailsDto>> Add(PositionDetailsDto positionDetailsDto)
        {
            var department = await _unitOfWork.Departments.GetByName(positionDetailsDto?.DepartmentName);
            var position = await _unitOfWork.Position.GetByName(positionDetailsDto?.PositionName);

            if (department == null)
            {
                return new DataResult<PositionDetailsDto>(ResultStatus.Error, "Seçili Departman Bulunamadı", null);
            }

            if (position != null)
            {
                return new DataResult<PositionDetailsDto>(ResultStatus.Error, "Pozisyon Halihazırda Mevcut", null);
            }

            var newPosition = new PositionDetailsDto()
            {
                DepartmentName = department.DepartmentName,
                PositionName = positionDetailsDto.PositionName
            };

            _unitOfWork.Position.Add(newPosition);

            return new DataResult<PositionDetailsDto>(ResultStatus.Success, newPosition.PositionName + "Başarıyla Eklendi", newPosition);

        }

        public async Task<IResult> Delete(int positionId, string modifiedByName)
        {
            var position = _unitOfWork.Position.GetById(positionId);


            if (position != null)
            {
                _unitOfWork.Position.Delete(positionId, modifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public async Task<IDataResult<List<PositionDetailsDto>>> GetAll()
        {
            var positions = _unitOfWork.Position.GetAllPositions();
            if (positions.Count > -1)
            {
                return new DataResult<List<PositionDetailsDto>>(ResultStatus.Success, positions);
            }
            return new DataResult<List<PositionDetailsDto>>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
        }

        public async Task<IResult> Update(PositionDetailsDto positionDetailsDto)
        {
            var department = await _unitOfWork.Departments.GetByName(positionDetailsDto?.DepartmentName);

            var position = _unitOfWork.Position.GetById(positionDetailsDto.PositionId);

            if (positionDetailsDto.PositionName == null)
            {
                positionDetailsDto.PositionName = position.Result.PositionName;
            }

            if (position != null)
            {

                var newPosition = new PositionUpdateDto();

                newPosition.Id = positionDetailsDto.PositionId;
                newPosition.Name = positionDetailsDto.PositionName;
                if (department != null)
                {
                    newPosition.DepartmentId = department.DepartmentId;
                }
                else
                {
                    newPosition.DepartmentId = null;
                }
             
                newPosition.ModifiedByName = positionDetailsDto.ModifiedByName;

                _unitOfWork.Position.Update(newPosition);
                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili pozisyon güncellenemedi");
        }
    }
}
