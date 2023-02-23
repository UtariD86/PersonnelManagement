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
    public class EfDepatmentRepository : EfEntityRepositoryBase<Department>, IDepartmentRepository
    {
        public EfDepatmentRepository(DbContext context) : base(context)
        {

        }

        public void Add(DepartmentDetailsDto departmentDetailsDto)
        {
            using (PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var department = new Department();
                
                department.Name = departmentDetailsDto.DepartmentName;
                department.IsDeleted = false;
                department.CreatedByName = "default";//şimdilik
                department.ModifiedByName = department.CreatedByName;
                department.CreatedDate = DateTime.Now;

                context.Departments.Add(department);
                context.SaveChanges();
            }
        }

        public List<DepartmentDetailsDto> GetAllDepartments()
        {
            using (PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var departments = from dep in context.Departments
                                  select new DepartmentDetailsDto
                                  {
                                      DepartmentId = dep.Id,
                                      DepartmentName = dep.Name,
                                      Positions = dep.Positions
                                  };
                return departments.ToList();
            }
        }

        async Task<DepartmentDetailsDto> IDepartmentRepository.GetByName(string departmentName)
        {
            using (PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var department = await context.Departments
                    .Where(d => d.Name == departmentName)
                    .Select(d => new DepartmentDetailsDto
                    {
                        DepartmentId = d.Id,
                        DepartmentName = d.Name,
                        Positions = d.Positions
                    })
                    .FirstOrDefaultAsync();

                return department;
            }
        }
    }
}
