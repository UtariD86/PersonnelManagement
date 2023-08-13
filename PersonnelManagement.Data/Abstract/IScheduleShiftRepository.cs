using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Abstract;

namespace PersonnelManagement.Data.Abstract
{
    public interface IScheduleShiftRepository: IEntityRepository<ScheduleShift>
    {
        List<ScheduleShiftDetailsDto> GetAllScheduleShifts();
        List<ScheduleShiftDetailsDto> GetAllScheduleShiftsById(int id);
        ScheduleShiftDetailsDto GetNextScheduleShift(int id);
        void Add(ScheduleShiftDetailsDto scheduleShiftDetailsDto);
        public Task<ScheduleShiftDetailsDto> GetById(int scheduleShiftId);
        void Delete(int scheduleShiftId, string modifiedByName);
        public Task<int> SaveAsync();
        void Update(ScheduleShiftDetailsDto scheduleShiftDetailsDto);
    }
}
