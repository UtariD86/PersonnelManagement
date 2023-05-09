using Azure.Core.GeoJson;
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
    public class ScheduleShiftManager : IScheduleShiftService
    {
        //IScheduleShiftRepository _scheduleShiftRepository;
        //IShiftRepository _shiftRepository;
        //IShiftTypeRepository _shiftTypeRepository;
        //IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleShiftManager(/*IScheduleShiftRepository scheduleShiftRepository, IShiftRepository shiftRepository, IEmployeeRepository employeeRepository, IShiftTypeRepository shiftTypeRepository,*/ IUnitOfWork unitOfWork)
        {
            //_scheduleShiftRepository = scheduleShiftRepository;
            //_shiftRepository = shiftRepository;
            //_employeeRepository = employeeRepository;
            //_shiftTypeRepository = shiftTypeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<ScheduleShiftDetailsDto>> Add(ScheduleShiftDetailsDto scheduleShiftDetailsDto)
        {
            var employee = _unitOfWork.Employees.GetById(scheduleShiftDetailsDto.EmployeeId).Result;
            var shiftTypeId = -1;
            if(scheduleShiftDetailsDto.IsSpecial == true)
            {
                var newSpecialShiftType = new ShiftTypeDetailsDto();
                newSpecialShiftType.ShiftTypeName = scheduleShiftDetailsDto.SpecialShiftType;//"Special Shift for " + employee.Name;
                newSpecialShiftType.StartTime = scheduleShiftDetailsDto.SpecialStartTime;
                newSpecialShiftType.EndTime = scheduleShiftDetailsDto.SpecialEndTime;
                newSpecialShiftType.Color = scheduleShiftDetailsDto.SpecialColor;
                newSpecialShiftType.ModifiedByName = scheduleShiftDetailsDto.ModifiedByName;

                var shiftType = _unitOfWork.ShiftTypes.Add(newSpecialShiftType).Result;

                shiftTypeId = shiftType.Id;

            }
            else
            {
                shiftTypeId = scheduleShiftDetailsDto.ShiftTypeId;
            }
            
            if(shiftTypeId != -1)
            {
                var newShift = new ShiftDetailsDto();
                newShift.ShiftTypeId = shiftTypeId;
                newShift.EmployeeId= scheduleShiftDetailsDto.EmployeeId;

                var shift = _unitOfWork.Shifts.Add(newShift).Result;
                await _unitOfWork.Shifts.SaveAsync();

                var newScheduleShift = new ScheduleShiftDetailsDto();
                newScheduleShift.ShiftId = shift.Id;
                newScheduleShift.StartDate = scheduleShiftDetailsDto.StartDate;
                newScheduleShift.EndDate= scheduleShiftDetailsDto.EndDate;

                _unitOfWork.ScheduleShifts.Add(newScheduleShift);
                _unitOfWork.ScheduleShifts.SaveAsync();
                return new DataResult<ScheduleShiftDetailsDto>(ResultStatus.Success, "Planlanmış Vardiya Başarıyla Eklendi", newScheduleShift);
            }
            else
            {
                return new DataResult<ScheduleShiftDetailsDto>(ResultStatus.Error, "Planlanmış Vardiya Eklenemedi", null);
            }
        }

        public async Task<IResult> Delete(int scheduleShiftId, string modifiedByName)
        {
            var scheduleShift = _unitOfWork.ScheduleShifts.GetById(scheduleShiftId);


            if (scheduleShift != null)
            {
                _unitOfWork.ScheduleShifts.Delete(scheduleShiftId, modifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public async Task<IDataResult<List<CalendarDto>>> GetAll()
        {
            var scheduleShifts = _unitOfWork.ScheduleShifts.GetAllScheduleShifts();
            if (scheduleShifts.Count > -1)
            {
                var CalendarDatas = new List<CalendarDto>();
                for (int i = 0; i < scheduleShifts.Count; i++){
                    var currentEmp = _unitOfWork.Employees.GetById(scheduleShifts[i].EmployeeId).Result;
                    var currentSt =  _unitOfWork.ShiftTypes.GetById(scheduleShifts[i].ShiftTypeId).Result;
                    var calendarDesc = currentSt.StartTime.Value.ToString(@"hh\:mm") + " - " + currentSt.EndTime.Value.ToString(@"hh\:mm");
                    var newCalendar = new CalendarDto();
                    newCalendar.id = scheduleShifts[i].ScheduleShiftId.ToString();
                    newCalendar.title = currentEmp.Name + " / " + currentSt.ShiftTypeName;
                    newCalendar.description = calendarDesc;
                    newCalendar.start = scheduleShifts[i].StartDate.Value.ToString("yyyy-MM-dd") + "T" + currentSt.StartTime.Value.ToString(@"hh\:mm\:ss");
                    newCalendar.end = scheduleShifts[i].EndDate.Value.ToString("yyyy-MM-dd") + "T" + currentSt.EndTime.Value.ToString(@"hh\:mm\:ss");
                    newCalendar.color = currentSt.Color;
                   
                    CalendarDatas.Add(newCalendar);
                }
                return new DataResult<List<CalendarDto>>(ResultStatus.Success, CalendarDatas);
            }
            return new DataResult<List<CalendarDto>>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
        }

        public async Task<IResult> Update(ScheduleShiftDetailsDto scheduleShiftDetailsDto)
        {
            var scheduleShift = _unitOfWork.ScheduleShifts.GetById(scheduleShiftDetailsDto.ScheduleShiftId);


            if (scheduleShift != null)
            {
                //_scheduleShiftRepository.Delete(scheduleShiftId, modifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }
    }
}
