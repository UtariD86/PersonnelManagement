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
        IShiftTypeRepository _shiftTypeRepository;
        public ShiftTypeManager(IShiftTypeRepository shiftTypeRepository)
        {
            _shiftTypeRepository = shiftTypeRepository;
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

            _shiftTypeRepository.Add(newShiftType);

            return new DataResult<ShiftTypeDetailsDto>(ResultStatus.Success, newShiftType.ShiftTypeName + "Başarıyla Eklendi", newShiftType);
        }

        public async Task<IResult> Delete(int shiftTypeId, string modifiedByName)
        {
            var shiftType = _shiftTypeRepository.GetById(shiftTypeId);


            if (shiftType != null)
            {
                _shiftTypeRepository.Delete(shiftTypeId, modifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili Vardiya Tipi bulunamadı");
        }

        public async Task<IDataResult<List<ShiftTypeDetailsDto>>> GetAll()
        {
            var shiftTypes = _shiftTypeRepository.GetAllShiftTypes();
            if (shiftTypes.Count > -1)
            {
                return new DataResult<List<ShiftTypeDetailsDto>>(ResultStatus.Success, shiftTypes);
            }
            return new DataResult<List<ShiftTypeDetailsDto>>(ResultStatus.Error, "Hiç Departman bulunamadı", null);
        }

        public async Task<IDataResult<ShiftTypeDetailsDto>> GetByName(string shiftTypeName)
        {
            var shifType = await _shiftTypeRepository.GetByName(shiftTypeName);
            if (shiftTypeName != null)
            {
                return new DataResult<ShiftTypeDetailsDto>(ResultStatus.Success, shifType);
            }
            return new DataResult<ShiftTypeDetailsDto>(ResultStatus.Error, "Departman bulunamadı", null);
        }

        public Task<IResult> Update(ShiftTypeDetailsDto shiftTypeDetailsDto)
        {
            throw new NotImplementedException();
        }
    }
}
