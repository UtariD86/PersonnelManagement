using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Concrete.EntityFramework;

namespace PersonnelManagement.Data.Concrete.Repositories
{
    public class EfShiftTypeRepository : EfEntityRepositoryBase<ShiftType>, IShiftTypeRepository
    {
        private readonly PersonnelManagerContext context;

        public EfShiftTypeRepository(PersonnelManagerContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<ShiftType> Add(ShiftTypeDetailsDto shiftTypeDetailsDto)
        {
            
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shiftType = new ShiftType();

                shiftType.Name = shiftTypeDetailsDto.ShiftTypeName;
                shiftType.StartTime = shiftTypeDetailsDto.StartTime;
                shiftType.EndTime = shiftTypeDetailsDto.EndTime;
                shiftType.Color = shiftTypeDetailsDto.Color;
                shiftType.CreatedByName = shiftTypeDetailsDto.ModifiedByName;
                shiftType.ModifiedByName = shiftTypeDetailsDto.ModifiedByName;
                shiftType.CreatedDate = DateTime.Now;
                shiftType.ModifiedDate = DateTime.Now;

                context.ShiftTypes.Add(shiftType);
                context.SaveChanges();
                return shiftType;

            //}
            
        }

        public async void Delete(int shiftTypeId, string modifiedByName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shiftType = await context.ShiftTypes.FindAsync(shiftTypeId);
                if (shiftType != null)
                {
                    shiftType.IsDeleted = true;
                    shiftType.ModifiedByName = modifiedByName;
                    shiftType.ModifiedDate = DateTime.Now;

                    context.ShiftTypes.Update(shiftType);
                    await context.SaveChangesAsync();
                }
            //}
        }

        public List<ShiftTypeDetailsDto> GetAllShiftTypes()
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shiftTypes = from st in context.ShiftTypes
                                  where (st.IsDeleted == false)
                                  select new ShiftTypeDetailsDto
                                  {
                                      ShiftTypeId = st.Id,
                                      ShiftTypeName = st.Name,
                                      StartTime = st.StartTime,
                                      EndTime = st.EndTime,
                                      Color = st.Color,
                                  };
                return shiftTypes.ToList();
            //}
        }

        public async Task<ShiftTypeDetailsDto> GetById(int shiftTypeId)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shiftType = await context.ShiftTypes
                    .Where(st => st.Id == shiftTypeId)
                    .Select(st => new ShiftTypeDetailsDto
                    {
                        ShiftTypeId = st.Id,
                        ShiftTypeName = st.Name,
                        StartTime = st.StartTime,
                        EndTime = st.EndTime,
                        Color = st.Color,

                    })
                    .FirstOrDefaultAsync();

                return shiftType;
            //}
        }

        public async Task<ShiftTypeDetailsDto> GetByName(string shiftTypeName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shiftType = await context.ShiftTypes
                    .Where(st => st.Name == shiftTypeName)
                    .Select(st => new ShiftTypeDetailsDto
                    {
                        ShiftTypeId = st.Id,
                        ShiftTypeName = st.Name,
                        StartTime = st.StartTime,
                        EndTime = st.EndTime,
                        Color = st.Color,
                    })
                    .FirstOrDefaultAsync();

                return shiftType;
            //}
        }

        public async void Update(ShiftTypeDetailsDto shiftTypeDetailsDto)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var shiftType = await context.ShiftTypes.FindAsync(shiftTypeDetailsDto.ShiftTypeId);
                if (shiftType != null)
                {
                    shiftType.Name = shiftTypeDetailsDto.ShiftTypeName;
                    shiftType.StartTime = shiftTypeDetailsDto.StartTime;
                    shiftType.EndTime = shiftTypeDetailsDto.EndTime;
                    shiftType.Color = shiftTypeDetailsDto.Color;
                    shiftType.ModifiedByName= shiftTypeDetailsDto.ModifiedByName;
                    shiftType.ModifiedDate = DateTime.Now;

                    context.ShiftTypes.Update(shiftType);
                    context.SaveChanges();
                }
            //}
        }
    }
}
