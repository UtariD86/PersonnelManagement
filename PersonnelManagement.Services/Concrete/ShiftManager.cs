using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Repositories;
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
    public class ShiftManager : IShiftService
    {
        IShiftRepository _shiftRepository;
        IEmployeeRepository _employeeRepository;
        IShiftTypeRepository _shiftTypeRepository;
        public ShiftManager(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository, IShiftTypeRepository shiftTypeRepository)
        {
            _shiftRepository = shiftRepository;
            _employeeRepository = employeeRepository;
            _shiftTypeRepository = shiftTypeRepository;
        }

        public async Task<IDataResult<ShiftDetailsDto>> Add(ShiftDetailsDto shiftDetailsDto)
        {
            var employee = await _employeeRepository.GetById(shiftDetailsDto.EmployeeId);
            var newShift = new ShiftDetailsDto()
            {
                EmployeeId = shiftDetailsDto.EmployeeId,
                ShiftTypeId = shiftDetailsDto.ShiftTypeId,
            };

            _shiftRepository.Add(newShift);

            return new DataResult<ShiftDetailsDto>(ResultStatus.Success, employee.Name + "için vardiya Başarıyla Eklendi", newShift);
        }

        public async Task<IResult> Delete(ShiftDetailsDto shiftDetailsDto)
        {
            var shift = await _shiftRepository.GetById(shiftDetailsDto.ShiftId);


            if (shift != null)
            {
                _shiftRepository.Delete(shiftDetailsDto.ShiftId, shiftDetailsDto.ModifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili vardiya bulunamadı");
        }

        public async Task<IDataResult<List<ShiftDetailsDto>>> GetAll()
        {
            var shifts = _shiftRepository.GetAllShifts();
            if (shifts.Count > -1)
            {
                return new DataResult<List<ShiftDetailsDto>>(ResultStatus.Success, shifts);
            }
            return new DataResult<List<ShiftDetailsDto>>(ResultStatus.Error, "Hiç vardiya bulunamadı", null);
        }

        public async Task<IResult> Update(ShiftDetailsDto shiftDetailsDto)
        {
            var shiftType = _shiftTypeRepository.GetById(shiftDetailsDto.ShiftTypeId);
            var employee = _employeeRepository.GetById(shiftDetailsDto.EmployeeId);

            var shift = _shiftRepository.GetById(shiftDetailsDto.ShiftId);

            if (shift != null)
            {

                var newShift = new ShiftDetailsDto();

                newShift.ShiftId = shiftDetailsDto.ShiftId;
                newShift.ShiftTypeId = shiftType.Id;
                newShift.EmployeeId= employee.Id;
                newShift.ModifiedByName = shiftDetailsDto.ModifiedByName;

                //_shiftRepository.Update(newShift);
                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili vardiya güncellenemedi");
        }
    }
}
