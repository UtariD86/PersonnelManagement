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
    public class EfPositionRepository : EfEntityRepositoryBase<Position>, IPositionRepository
    {
        private readonly PersonnelManagerContext context;

        public EfPositionRepository(PersonnelManagerContext _context) : base(_context)
        {
            context = _context;
        }

        public void Add(PositionDetailsDto positionDetailsDto)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var position = new Position();
                var department = context.Departments.FirstOrDefault(d => d.Name == positionDetailsDto.DepartmentName);
                if (department != null)
                {
                    position.Name = positionDetailsDto.PositionName;
                    position.DepartmentId = department.Id;
                    position.IsDeleted = false;
                    position.CreatedByName = "default";//şimdilik
                    position.ModifiedByName = position.CreatedByName;
                    position.CreatedDate = DateTime.Now;

                    context.Positions.Add(position);
                    context.SaveChanges();
                }
            //}
        }

        public async void Delete(int positionId, string modifiedByName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var position = await context.Positions.FindAsync(positionId);
                if (position != null)
                {
                    position.IsDeleted = true;
                    position.ModifiedByName = modifiedByName;
                    position.ModifiedDate = DateTime.Now;

                    context.Positions.Update(position);
                    context.SaveChanges();
                }
            //}
        }

        public async Task<PositionDetailsDto> GetById(int positionId)
        {
            //using(PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var position = await context.Positions
                    .Where(p => p.Id == positionId)
                    .Select(p => new PositionDetailsDto
                    {
                        PositionId = p.Id
                    })
                    .FirstOrDefaultAsync();

                return position;
            //}
        }

        public async Task<PositionDetailsDto> GetByName(string positionName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var position = await context.Positions
                    .Where(p => p.Name == positionName)
                    .Select(p => new PositionDetailsDto
                    {
                        PositionId = p.Id,
                        PositionName = p.Name,
                        DepartmentName = p.Department.Name
                    })
                    .FirstOrDefaultAsync();

                return position;
            //}
        }

        public async void Update(PositionUpdateDto positionUpdateDto)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var position = await context.Positions.FindAsync(positionUpdateDto.Id);
                if (position != null)
                {
                    position.Name = positionUpdateDto.Name;
                    if (positionUpdateDto.DepartmentId != null)
                    {
                        position.DepartmentId = (int)positionUpdateDto.DepartmentId;
                    }
                  
                    position.ModifiedByName = positionUpdateDto.ModifiedByName;
                    position.ModifiedDate = DateTime.Now;

                    context.Positions.Update(position);
                    context.SaveChanges();
                }
            //}
        }

        List<PositionDetailsDto> IPositionRepository.GetAllPositions()
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var positions = from pos in context.Positions
                                  join d in context.Departments on pos.DepartmentId equals d.Id
                                where (pos.IsDeleted == false && d.IsDeleted == false)
                                select new PositionDetailsDto
                                  {
                                      PositionId = pos.Id,
                                      PositionName = pos.Name,
                                      DepartmentName = d.Name
                                  };
                return positions.ToList();
            //}
        }
    }
}
