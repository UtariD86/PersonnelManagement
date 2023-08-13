using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IScheduleShiftService
    {
        Task<IDataResult<List<CalendarDto>>> GetAll();
        Task<IDataResult<NextShiftDetailsDto>> GetNext(int id);
        Task<IDataResult<List<CalendarDto>>> GetAllById(int id);
        Task<IDataResult<ScheduleShift>> Add(ScheduleShift scheduleShift);
        Task<IResult> Delete(ScheduleShift scheduleShift);
        Task<IResult> Update(ScheduleShiftDetailsDto scheduleShiftDetailsDto);
    }
}
