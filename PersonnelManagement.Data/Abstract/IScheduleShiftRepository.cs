using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Abstract
{
    public interface IScheduleShiftRepository
    {
        List<ScheduleShiftDetailsDto> GetAllScheduleShifts();
        void Add(ScheduleShiftDetailsDto scheduleShiftDetailsDto);
        public Task<ScheduleShiftDetailsDto> GetById(int scheduleShiftId);
        void Delete(int scheduleShiftId, string modifiedByName);
        public Task<int> SaveAsync();
        void Update(ScheduleShiftDetailsDto scheduleShiftDetailsDto);
    }
}
