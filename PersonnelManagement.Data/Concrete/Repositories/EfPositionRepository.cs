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

        List<PositionDetailsDto> IPositionRepository.GetAllPositions()
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
            var positions = from pos in context.Positions
                            join d in context.Departments on pos.DepartmentId equals d.Id
                            where (pos.IsDeleted == false && d.IsDeleted == false)
                            select new PositionDetailsDto
                            {
                                Id = pos.Id,
                                Name = pos.Name,
                                DepartmentName = d.Name
                            };
            return positions.ToList();
            //}
        }
    }
}
