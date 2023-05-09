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
    
    
    public class ShiftTypeManager : IShiftTypeService
    {
        //IShiftTypeRepository _shiftTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ShiftTypeManager(IUnitOfWork unitOfWork)
        {
            //_shiftTypeRepository = shiftTypeRepository;
            _unitOfWork= unitOfWork;
        }
        
        public async Task<IDataResult<ShiftTypeDetailsDto>> Add(ShiftTypeDetailsDto shiftTypeDetailsDto)
        {
            
            var newShiftType = new ShiftTypeDetailsDto()
            {
                ShiftTypeName= shiftTypeDetailsDto.ShiftTypeName,
                StartTime = shiftTypeDetailsDto.StartTime,
                EndTime= shiftTypeDetailsDto.EndTime,
                Color= shiftTypeDetailsDto.Color,
                ModifiedByName= shiftTypeDetailsDto.ModifiedByName,
            };

            _unitOfWork.ShiftTypes.Add(newShiftType);

            return new DataResult<ShiftTypeDetailsDto>(ResultStatus.Success, newShiftType.ShiftTypeName + "Başarıyla Eklendi", newShiftType);
        }

        public async Task<IResult> Delete(int shiftTypeId, string modifiedByName)
        {
            var shiftType = _unitOfWork.ShiftTypes.GetById(shiftTypeId);


            if (shiftType != null)
            {
                _unitOfWork.ShiftTypes.Delete(shiftTypeId, modifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili Vardiya Tipi bulunamadı");
        }

        public async Task<IDataResult<List<ShiftTypeDetailsDto>>> GetAll()
        {
            var shiftTypes = _unitOfWork.ShiftTypes.GetAllShiftTypes();
            if (shiftTypes.Count > -1)
            {
                return new DataResult<List<ShiftTypeDetailsDto>>(ResultStatus.Success, shiftTypes);
            }
            return new DataResult<List<ShiftTypeDetailsDto>>(ResultStatus.Error, "Hiç vardiya tipi bulunamadı", null);
        }

        public async Task<IDataResult<ShiftTypeDetailsDto>> GetByName(string shiftTypeName)
        {
            var shifType = await _unitOfWork.ShiftTypes.GetByName(shiftTypeName);
            if (shiftTypeName != null)
            {
                return new DataResult<ShiftTypeDetailsDto>(ResultStatus.Success, shifType);
            }
            return new DataResult<ShiftTypeDetailsDto>(ResultStatus.Error, "Vardiya Tipi bulunamadı", null);
        }

        public async Task<IResult> Update(ShiftTypeDetailsDto shiftTypeDetailsDto)
        {
            var shiftType = _unitOfWork.ShiftTypes.GetById(shiftTypeDetailsDto.ShiftTypeId);


            if (shiftType != null)
            {
                var newShiftType = new ShiftTypeDetailsDto();

                newShiftType.ShiftTypeId = shiftTypeDetailsDto.ShiftTypeId;
                newShiftType.ShiftTypeName = shiftTypeDetailsDto.ShiftTypeName;
                newShiftType.StartTime= shiftTypeDetailsDto.StartTime;
                newShiftType.EndTime = shiftTypeDetailsDto.EndTime;
                newShiftType.ModifiedByName = shiftTypeDetailsDto.ModifiedByName;
                newShiftType.Color = shiftTypeDetailsDto.Color;

                _unitOfWork.ShiftTypes.Update(newShiftType);
                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili Vardiya Tipi bulunamadı");
        }
    }
}
