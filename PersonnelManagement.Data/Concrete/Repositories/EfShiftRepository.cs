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
    public class EfShiftRepository : EfEntityRepositoryBase<Shift>, IShiftRepository
    {
        private readonly PersonnelManagerContext context;

        public EfShiftRepository(PersonnelManagerContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<Shift> Add(ShiftDetailsDto shiftDetailsDto)
        {
                var shift = new Shift();

                shift.EmployeeId = shiftDetailsDto.EmployeeId;
                shift.ShiftTypeId= shiftDetailsDto.ShiftTypeId;
                shift.CreatedByName = "try";
                shift.ModifiedByName = "try";
                shift.CreatedDate = DateTime.Now;
                shift.ModifiedDate = DateTime.Now;

                //context.Shifts.Add(shift);
                context.Set<Shift>().Add(shift);

                return shift;

        }

        public async void Delete(int shiftId, string modifiedByName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shift = await context.Shifts.FindAsync(shiftId);
                if (shift != null)
                {
                    shift.IsDeleted = true;
                    shift.ModifiedByName = modifiedByName;
                    shift.ModifiedDate = DateTime.Now;

                    context.Shifts.Update(shift);
                    context.SaveChanges();
                }
            //}
        }

        public List<ShiftDetailsDto> GetAllShifts()
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shifts = from s in context.Shifts
                                join e in context.Employees on s.EmployeeId equals e.Id
                                join st in context.ShiftTypes on s.ShiftTypeId equals st.Id
                                join d in context.Departments on e.DepartmentId equals d.Id
                                join p in context.Positions on e.PositionId equals p.Id
                             where (s.IsDeleted == false && e.IsDeleted == false && st.IsDeleted == false)
                                select new ShiftDetailsDto
                                {
                                    ShiftId = s.Id,
                                    ShiftTypeId = st.Id,
                                    ShiftTypeName = st.Name,
                                    StartTime = st.StartTime,
                                    EndTime = st.EndTime,
                                    Color = st.Color,
                                    EmployeeId = e.Id,
                                    EmployeeName = e.Name,
                                    DepartmentName = d.Name,
                                    PositionName = p.Name,
                                };
                return shifts.ToList();
            //}
        }

        public async Task<ShiftDetailsDto> GetById(int shiftId)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shift = await context.Shifts
                    .Where(s => s.Id == shiftId)
                    .Select(s => new Shift
                    {
                        Id = s.Id,
                        EmployeeId = s.EmployeeId,
                        ShiftTypeId = s.ShiftTypeId,
                    })
                    .FirstOrDefaultAsync();

                var shiftDetails = new ShiftDetailsDto
                {
                    ShiftId = shift.Id,
                    EmployeeId = shift.EmployeeId,
                    ShiftTypeId = shift.ShiftTypeId,
                };

                return shiftDetails;
            //}
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
         
        }

        public async void Update(ShiftDetailsDto shiftDetailsDto)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shift = await context.Shifts.FindAsync(shiftDetailsDto.ShiftId);
                if (shift != null)
                {
                    shift.EmployeeId = shiftDetailsDto.EmployeeId;
                    shift.ShiftTypeId = shiftDetailsDto.ShiftTypeId;
                    shift.CreatedByName = "try";
                    shift.ModifiedByName = "try";
                    shift.CreatedDate = DateTime.Now;
                    shift.ModifiedDate = DateTime.Now;

                    context.Shifts.Update(shift);
                    context.SaveChanges();
                }
            //}
        }
    }
}
