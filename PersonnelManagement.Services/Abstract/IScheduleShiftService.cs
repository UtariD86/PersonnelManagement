using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;

namespace PersonnelManagement.Services.Abstract
{
    public interface IScheduleShiftService
    {
        Task<IDataResult<List<CalendarDto>>> GetAll();
        Task<IDataResult<ScheduleShiftDetailsDto>> Add(ScheduleShiftDetailsDto scheduleShiftDetailsDto);
        Task<IResult> Delete(int scheduleShiftId, string modifiedByName);
        //Task<IResult> Update(ScheduleShiftDetailsDto scheduleShiftDetailsDto);
    }
}
