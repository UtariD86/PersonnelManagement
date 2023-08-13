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
using zurafworks.Shared.Results.Abstract;
using zurafworks.Shared.Results.ComplexTypes;
using zurafworks.Shared.Results.Concrete;

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
        public async Task<IDataResult<ScheduleShift>> Add(ScheduleShift scheduleShift)
        {
            //var employee = _unitOfWork.Employees.GetAsync(e => e.Id == scheduleShiftDetailsDto.EmployeeId);
            //var shiftTypeId = -1;
            //if(scheduleShiftDetailsDto.IsSpecial == true)
            //{
            //    var newSpecialShiftType = new ShiftTypeDetailsDto();
            //    newSpecialShiftType.ShiftTypeName = scheduleShiftDetailsDto.SpecialShiftType;//"Special Shift for " + employee.Name;
            //    newSpecialShiftType.StartTime = scheduleShiftDetailsDto.SpecialStartTime;
            //    newSpecialShiftType.EndTime = scheduleShiftDetailsDto.SpecialEndTime;
            //    newSpecialShiftType.Color = scheduleShiftDetailsDto.SpecialColor;
            //    newSpecialShiftType.ModifiedByName = scheduleShiftDetailsDto.ModifiedByName;

            //    var shiftType = _unitOfWork.ShiftTypes.Add(newSpecialShiftType).Result;

            //    shiftTypeId = shiftType.Id;

            //}
            //else
            //{
            //    shiftTypeId = scheduleShiftDetailsDto.ShiftTypeId;
            //}

            //if(shiftTypeId != -1)
            //{
            //    var newShift = new Shift();
            //    newShift.ShiftTypeId = shiftTypeId;
            //    newShift.EmployeeId= scheduleShiftDetailsDto.EmployeeId;

            //    var shift = await _unitOfWork.Shifts.AddAsync(newShift);
            //    //await _unitOfWork.Shifts.SaveAsync();

            //    var newScheduleShift = new ScheduleShift();
            //    newScheduleShift.ShiftId = shift.Id;
            //    newScheduleShift.StartDate = scheduleShiftDetailsDto.StartDate;
            //    newScheduleShift.EndDate= scheduleShiftDetailsDto.EndDate;
            if (scheduleShift != null)
            {
                await _unitOfWork.ScheduleShifts.AddAsync(scheduleShift);
                //    //await _unitOfWork.ScheduleShifts.SaveAsync();
                await _unitOfWork.SaveChangesAsync();// System.ObjectDisposedException çöz bunu
                return new DataResult<ScheduleShift>(ResultStatus.Success, "Planlanmış Vardiya Başarıyla Eklendi", scheduleShift);
            }
            else
            {
                return new DataResult<ScheduleShift>(ResultStatus.Error, "Planlanmış Vardiya Eklenemedi", null);
            }
        }

        public async Task<IResult> Delete(ScheduleShift scheduleShift)
        {
            var _scheduleShift = _unitOfWork.ScheduleShifts.Get(scheduleShift.Id);
            if (_scheduleShift != null)
            {
                _scheduleShift.IsDeleted = true;
                _scheduleShift.ModifiedByName = scheduleShift.ModifiedByName;
                _scheduleShift.ModifiedDate = DateTime.Now;
                _unitOfWork.ScheduleShifts.Update(_scheduleShift);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili Planlanmış vardiya bulunamadı");
        }

        public async Task<IDataResult<List<CalendarDto>>> GetAll()
        {
            var scheduleShifts = _unitOfWork.ScheduleShifts.GetAllScheduleShifts();
            if (scheduleShifts.Count > -1)
            {
                var CalendarDatas = new List<CalendarDto>();
                for (int i = 0; i < scheduleShifts.Count; i++)
                {
                    var currentEmp = _unitOfWork.Employees.Get(scheduleShifts[i].EmployeeId);
                    var currentSt = _unitOfWork.ShiftTypes.GetById(scheduleShifts[i].ShiftTypeId).Result;
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

        public async Task<IDataResult<NextShiftDetailsDto>> GetNext(int id)
        {
            var scheduleShift = _unitOfWork.ScheduleShifts.GetNextScheduleShift(id);
            if (scheduleShift != null)
            {
                
                //var calendarDesc = currentSt.StartTime.Value.ToString(@"hh\:mm") + " - " + currentSt.EndTime.Value.ToString(@"hh\:mm");
                var nextShift = new NextShiftDetailsDto();
                if(scheduleShift.IsSpecial != true)
                {
                    var currentEmp = _unitOfWork.Employees.Get(scheduleShift.EmployeeId);
                    var currentSt = _unitOfWork.ShiftTypes.GetById(scheduleShift.ShiftTypeId).Result;
                    nextShift.ShiftTypeName = currentSt.ShiftTypeName;
                    nextShift.ScheduleShiftId = scheduleShift.ScheduleShiftId;
                    nextShift.ShiftId = scheduleShift.ShiftId;
                    nextShift.EmployeeId = scheduleShift.EmployeeId;
                    nextShift.ShiftTypeId = scheduleShift.ShiftTypeId;
                    nextShift.StartDate= scheduleShift.StartDate;
                    nextShift.EndDate= scheduleShift.EndDate;
                    nextShift.StartTime = currentSt.StartTime;
                    nextShift.EndTime = currentSt.EndTime;
                    nextShift.ModifiedByName = scheduleShift.ModifiedByName;
                    nextShift.Color = currentSt.Color;
                    nextShift.Enter = scheduleShift.Enter;
                    nextShift.Exit = scheduleShift.Exit;
                }
                else
                {

                    nextShift.ShiftTypeName = scheduleShift.SpecialShiftType;
                }

                return new DataResult<NextShiftDetailsDto>(ResultStatus.Success, nextShift);
            }
            return new DataResult<NextShiftDetailsDto>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
        }

        public async Task<IDataResult<List<CalendarDto>>> GetAllById(int id)
        {
            var scheduleShifts = _unitOfWork.ScheduleShifts.GetAllScheduleShiftsById(id);
            if (scheduleShifts.Count > -1)
            {
                var CalendarDatas = new List<CalendarDto>();
                for (int i = 0; i < scheduleShifts.Count; i++)
                {
                    var currentEmp = _unitOfWork.Employees.Get(scheduleShifts[i].EmployeeId);
                    var currentSt = _unitOfWork.ShiftTypes.GetById(scheduleShifts[i].ShiftTypeId).Result;
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
