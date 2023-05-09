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
        //IShiftRepository _shiftRepository;
        //IEmployeeRepository _employeeRepository;
        //IShiftTypeRepository _shiftTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ShiftManager(/*IShiftRepository shiftRepository, IEmployeeRepository employeeRepository, IShiftTypeRepository shiftTypeRepository,*/ IUnitOfWork unitOfWork)
        {
            //_shiftRepository = shiftRepository;
            //_employeeRepository = employeeRepository;
            //_shiftTypeRepository = shiftTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<ShiftDetailsDto>> Add(ShiftDetailsDto shiftDetailsDto)
        {
            var employee = await _unitOfWork.Employees.GetById(shiftDetailsDto.EmployeeId);
            var newShift = new ShiftDetailsDto()
            {
                EmployeeId = shiftDetailsDto.EmployeeId,
                ShiftTypeId = shiftDetailsDto.ShiftTypeId,
            };

            _unitOfWork.Shifts.Add(newShift);

            return new DataResult<ShiftDetailsDto>(ResultStatus.Success, employee.Name + "için vardiya Başarıyla Eklendi", newShift);
        }

        public async Task<IResult> Delete(ShiftDetailsDto shiftDetailsDto)
        {
            var shift = await _unitOfWork.Shifts.GetById(shiftDetailsDto.ShiftId);


            if (shift != null)
            {
                _unitOfWork.Shifts.Delete(shiftDetailsDto.ShiftId, shiftDetailsDto.ModifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili vardiya bulunamadı");
        }

        public async Task<IDataResult<List<ShiftDetailsDto>>> GetAll()
        {
            var shifts = _unitOfWork.Shifts.GetAllShifts();
            if (shifts.Count > -1)
            {
                return new DataResult<List<ShiftDetailsDto>>(ResultStatus.Success, shifts);
            }
            return new DataResult<List<ShiftDetailsDto>>(ResultStatus.Error, "Hiç vardiya bulunamadı", null);
        }

        public async Task<IResult> Update(ShiftDetailsDto shiftDetailsDto)
        {
            var shiftType = _unitOfWork.ShiftTypes.GetById(shiftDetailsDto.ShiftTypeId);
            var employee = _unitOfWork.Employees.GetById(shiftDetailsDto.EmployeeId);

            var shift = _unitOfWork.Shifts.GetById(shiftDetailsDto.ShiftId);

            if (shift != null)
            {

                var newShift = new ShiftDetailsDto();

                newShift.ShiftId = shiftDetailsDto.ShiftId;
                newShift.ShiftTypeId = shiftType.Id;
                newShift.EmployeeId= employee.Id;
                newShift.ModifiedByName = shiftDetailsDto.ModifiedByName;

                _unitOfWork.Shifts.Update(newShift);
                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili vardiya güncellenemedi");
        }
    }
}
