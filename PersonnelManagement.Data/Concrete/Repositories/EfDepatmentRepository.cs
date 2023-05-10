using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
using static System.Formats.Asn1.AsnWriter;

namespace PersonnelManagement.Data.Concrete.Repositories
{
    public class EfDepartmentRepository : EfEntityRepositoryBase<Department>, IDepartmentRepository
    {
        private readonly PersonnelManagerContext context;
        private readonly IUnitOfWork unitOfWork;
        private readonly ServiceProvider serviceProvider;

        public EfDepartmentRepository(PersonnelManagerContext _context, IUnitOfWork unitOfWork) : base(_context)
        {
            context = _context;
            this.unitOfWork = unitOfWork;
        }


        public async void Add(DepartmentDetailsDto departmentDetailsDto)
        {
            //using (context/*PersonnelManagerContext context = new PersonnelManagerContext()*/)
            //{
            //using var scope = serviceProvider.CreateScope();
            //var dbContext = scope.ServiceProvider.GetRequiredService<PersonnelManagerContext>();
            var department = new Department();
                
                department.Name = departmentDetailsDto.DepartmentName;
                department.IsDeleted = false;
                department.CreatedByName = "default";//şimdilik
                department.ModifiedByName = department.CreatedByName;
                department.CreatedDate = DateTime.Now;

            context.Departments.Add(department);
            context.SaveChanges();
           
            //}
        }

        public async void Delete(int departmentId, string modifiedByName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var department = await context.Departments.FindAsync(departmentId);
                if (department != null)
                {
                    department.IsDeleted = true;
                    department.ModifiedByName = modifiedByName;
                    department.ModifiedDate = DateTime.Now;

                    context.Departments.Update(department);
                    context.SaveChanges();
                }
            //}
        }

        public List<DepartmentDetailsDto> GetAllDepartments()
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var departments = from dep in context.Departments
                                  where (dep.IsDeleted == false)
                                  select new DepartmentDetailsDto
                                  {
                                      DepartmentId = dep.Id,
                                      DepartmentName = dep.Name,
                                      Positions = dep.Positions
                                  };
            
            return departments.ToList();
            //}
        }

        public async Task<DepartmentDetailsDto> GetById(int departmentId)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var department = await context.Departments
                    .Where(e => e.Id == departmentId)
                    .Select(e => new DepartmentDetailsDto
                    {
                        DepartmentId = e.Id
                    })
                    .FirstOrDefaultAsync();

                return department;
            //}
        }

        public async void Update(DepartmentUpdateDto departmentUpdateDto)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var department = await context.Departments.FindAsync(departmentUpdateDto.Id);
                if (department != null)
                {
                    department.Name = departmentUpdateDto.Name;
                    department.ModifiedByName = departmentUpdateDto.ModifiedByName;
                    department.ModifiedDate = DateTime.Now;

                    context.Departments.Update(department);
                    context.SaveChanges();
                }
            //}
        }

        async Task<DepartmentDetailsDto> IDepartmentRepository.GetByName(string departmentName)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
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
            //}
        }
    }
}
