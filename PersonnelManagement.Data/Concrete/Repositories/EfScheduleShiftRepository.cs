using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Concrete.EntityFramework;

namespace PersonnelManagement.Data.Concrete.Repositories
{
    public class EfScheduleShiftRepository : EfEntityRepositoryBase<ScheduleShift>, IScheduleShiftRepository
    {
        private readonly PersonnelManagerContext context;

        public EfScheduleShiftRepository(PersonnelManagerContext _context) : base(_context)
        {
            context = _context;
        }
        public void Add(ScheduleShiftDetailsDto scheduleShiftDetailsDto)
        {
                var scheduleShift = new ScheduleShift();
                var shift = context.Shifts.FirstOrDefault(s => s.Id == scheduleShiftDetailsDto.ShiftId);
                if (shift != null)
                {
                    scheduleShift.ShiftId = shift.Id;
                    scheduleShift.StartDate = scheduleShiftDetailsDto?.StartDate;
                    scheduleShift.EndDate = scheduleShiftDetailsDto?.EndDate;
                    scheduleShift.ModifiedByName = "try";
                    scheduleShift.CreatedByName = "try";
                    scheduleShift.IsDeleted = false;
                    scheduleShift.CreatedDate = DateTime.Now;

                    context.ScheduleShifts.Add(scheduleShift);
                }
        }

        public async void Delete(int scheduleShiftId, string modifiedByName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var scheduleShift = await context.ScheduleShifts.FindAsync(scheduleShiftId);
                if (scheduleShift != null)
                {
                    scheduleShift.IsDeleted = true;
                    scheduleShift.ModifiedByName = modifiedByName;
                    scheduleShift.ModifiedDate = DateTime.Now;

                    context.ScheduleShifts.Update(scheduleShift);
                    context.SaveChanges();
                }
            //}
        }

        public List<ScheduleShiftDetailsDto> GetAllScheduleShifts()
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var scheduleShifts = from ss in context.ScheduleShifts
                                join s in context.Shifts on ss.ShiftId equals s.Id
                                join st in context.ShiftTypes on s.ShiftTypeId equals st.Id
                                join e in context.Employees on s.EmployeeId equals e.Id
                                where (ss.IsDeleted == false && st.IsDeleted == false && s.IsDeleted == false && e.IsDeleted == false)
                                select new ScheduleShiftDetailsDto
                                {
                                    ScheduleShiftId = ss.Id,
                                    ShiftId = ss.ShiftId,
                                    EmployeeId = s.EmployeeId,
                                    ShiftTypeId = s.ShiftTypeId,
                                    StartDate = ss.StartDate,
                                    EndDate = ss.EndDate,
                                    Enter = s.Enter,
                                    Exit = s.Exit,
                                };
                return scheduleShifts.ToList();
            //}
        }
        
        public List<ScheduleShiftDetailsDto> GetAllScheduleShiftsById(int id)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var scheduleShifts = from ss in context.ScheduleShifts
                                join s in context.Shifts on ss.ShiftId equals s.Id
                                join st in context.ShiftTypes on s.ShiftTypeId equals st.Id
                                join e in context.Employees on s.EmployeeId equals e.Id
                                where (ss.IsDeleted == false && st.IsDeleted == false && s.IsDeleted == false && e.IsDeleted == false && s.EmployeeId == id)
                                select new ScheduleShiftDetailsDto
                                {
                                    ScheduleShiftId = ss.Id,
                                    ShiftId = ss.ShiftId,
                                    EmployeeId = s.EmployeeId,
                                    ShiftTypeId = s.ShiftTypeId,
                                    StartDate = ss.StartDate,
                                    EndDate = ss.EndDate,
                                    Enter= s.Enter,
                                    Exit= s.Exit,
                                };
                return scheduleShifts.ToList();
            //}
        }
        
        public ScheduleShiftDetailsDto GetNextScheduleShift(int id)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
            var time = DateTime.Now.TimeOfDay;
            var date = DateTime.Now.Date;
            var scheduleShifts = from ss in context.ScheduleShifts
                                 join s in context.Shifts on ss.ShiftId equals s.Id
                                 join st in context.ShiftTypes on s.ShiftTypeId equals st.Id
                                 join e in context.Employees on s.EmployeeId equals e.Id
                                 where (ss.IsDeleted == false && st.IsDeleted == false && s.IsDeleted == false && e.IsDeleted == false
                                        && ((ss.EndDate > date) || (ss.EndDate == date && st.EndTime > time))
                                        && s.EmployeeId == id)
                                 orderby ss.EndDate, st.EndTime
                                 select new ScheduleShiftDetailsDto
                                 {
                                     ScheduleShiftId = ss.Id,
                                     ShiftId = ss.ShiftId,
                                     EmployeeId = s.EmployeeId,
                                     ShiftTypeId = s.ShiftTypeId,
                                     StartDate = ss.StartDate,
                                     EndDate = ss.EndDate,
                                     Enter = s.Enter,
                                     Exit = s.Exit
                                 };
            var orderedShifts = scheduleShifts.ToList();
            if (orderedShifts.Count > 0)
            {
                return orderedShifts[0];
            }
            else
            {
                // Liste boş, bu yüzden özel bir durum döndürüyoruz
                var notFound = new ScheduleShiftDetailsDto();
                notFound.SpecialShiftType = "Yaklaşan vardiya yok!";
                notFound.IsSpecial = true; //özel durum bulunamadı!
                return notFound;
            }
        }

        public async Task<ScheduleShiftDetailsDto> GetById(int scheduleShiftId)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var scheduleShift = await context.ScheduleShifts
                    .Where(ss => ss.Id == scheduleShiftId)
                    .Select(ss => new ScheduleShiftDetailsDto
                    {
                        ScheduleShiftId = ss.Id,
                        ShiftId= ss.ShiftId,
                        StartDate = ss.StartDate,
                        EndDate= ss.EndDate,

                    })
                    .FirstOrDefaultAsync();

                return scheduleShift;
            //}
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async void Update(ScheduleShiftDetailsDto scheduleShiftDetailsDto)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var scheduleShift = await context.ScheduleShifts.FindAsync(scheduleShiftDetailsDto.ScheduleShiftId);
                if (scheduleShift != null)
                {
                    scheduleShift.ShiftId = scheduleShiftDetailsDto.ShiftId;
                    scheduleShift.StartDate = scheduleShiftDetailsDto?.StartDate;
                    scheduleShift.EndDate = scheduleShiftDetailsDto?.EndDate;
                    scheduleShift.ModifiedByName = "try";
                    scheduleShift.CreatedByName = "try";
                    scheduleShift.IsDeleted = false;
                    scheduleShift.CreatedDate = DateTime.Now;

                    context.ScheduleShifts.Update(scheduleShift);
                    context.SaveChanges();
                }
            //}
        }
    }
}
