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
        public EfPositionRepository(DbContext context) : base(context)
        {
        }

        public void Add(PositionDetailsDto positionDetailsDto)
        {
            using (PersonnelManagerContext context = new PersonnelManagerContext())
            {
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
            }
        }

        public async Task<PositionDetailsDto> GetByName(string positionName)
        {
            using (PersonnelManagerContext context = new PersonnelManagerContext())
            {
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
            }
        }

        List<PositionDetailsDto> IPositionRepository.GetAllPositions()
        {
            using (PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var positions = from pos in context.Positions
                                  join d in context.Departments on pos.DepartmentId equals d.Id
                                  select new PositionDetailsDto
                                  {
                                      PositionId = pos.Id,
                                      PositionName = pos.Name,
                                      DepartmentName = d.Name
                                  };
                return positions.ToList();
            }
        }
    }
}
