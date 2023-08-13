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
using zurafworks.Shared.Results.Abstract;
using zurafworks.Shared.Results.ComplexTypes;
using zurafworks.Shared.Results.Concrete;

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

        public async Task<IDataResult<Shift>> Add(Shift shift)
        {
            var employee = await _unitOfWork.Employees.GetAsync( e=> e.Id == shift.EmployeeId);
            var newShift = new Shift()
            {
                EmployeeId = shift.EmployeeId,
                ShiftTypeId = shift.ShiftTypeId,
            };

            var addedShift = await _unitOfWork.Shifts.AddAsync(newShift);
            await _unitOfWork.SaveChangesAsync();

            return new DataResult<Shift>(ResultStatus.Success, employee.Name + "için vardiya Başarıyla Eklendi", addedShift);
        }

        public async Task<IResult> Delete(Shift shift)
        {
            var _shift = await _unitOfWork.Shifts.GetAsync(s=>s.Id == shift.Id);


            if (_shift != null)
            {
                _shift.IsDeleted= true;
                _shift.ModifiedByName = shift.ModifiedByName;
                _shift.ModifiedDate = DateTime.Now;
                await _unitOfWork.Shifts.UpdateAsync(_shift);
                await _unitOfWork.SaveChangesAsync();
                //_unitOfWork.Shifts.Delete(shiftDetailsDto.ShiftId, shiftDetailsDto.ModifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili vardiya bulunamadı");
        }

        public async Task<IDataResult<IList<Shift>>> GetAll()
        {
            var shifts = await _unitOfWork.Shifts.GetAllAsync(s => s.IsDeleted == false);
            if (shifts.Count > -1)
            {
                return new DataResult<IList<Shift>>(ResultStatus.Success, shifts);
            }
            return new DataResult<IList<Shift>>(ResultStatus.Error, "Hiç vardiya bulunamadı", null);
        }

        public async Task<IResult> Update(Shift shift)
        {
            var shiftType = await _unitOfWork.ShiftTypes.GetAsync(st => st.Id == shift.ShiftTypeId );
            var employee = await _unitOfWork.Employees.GetAsync(e => e.Id == shift.EmployeeId);

            var _shift = _unitOfWork.Shifts.GetAsync(st => st.Id == shift.Id);

            if (_shift != null && shiftType != null && employee != null)
            {
                await _unitOfWork.Shifts.UpdateAsync(shift);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili vardiya güncellenemedi");
        }

        public async Task<IDataResult<Shift>> GetById(int id)
        {
            var shift = _unitOfWork.Shifts.Get(id);

            if (shift != null)
            {
                return new DataResult<Shift>(ResultStatus.Success, shift);//dto to entity yüzünden hata çıktı
            }
            return new DataResult<Shift>(ResultStatus.Error, "Departman bulunamadı", null);
        }
    }
}
