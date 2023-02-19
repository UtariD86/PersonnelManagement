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
    public class EfEmployeeRepository : EfEntityRepositoryBase<Employee>, IEmployeeRepository
    {
        public EfEmployeeRepository(DbContext context) : base(context)
        {
        }

        public void Add(EmployeeDetailsDto employeeDetailsDto)
        {
            using(PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var employee = new Employee();
                var department = context.Departments.FirstOrDefault(d => d.Name == employeeDetailsDto.DepartmentName);
                var position = context.Positions.FirstOrDefault(p => p.Name == employeeDetailsDto.PositionName);
                if(department != null && position !=null)
                {
                    employee.Name = employeeDetailsDto.EmployeeName;
                    employee.DepartmentId = department.Id;
                    employee.PositionId = position.Id;
                    employee.IsDeleted = false;
                    employee.CreatedByName = "default";//şimdilik
                    employee.ModifiedByName = employee.CreatedByName;
                    employee.CreatedDate = DateTime.Now;

                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
            }
        }

        public List<EmployeeDetailsDto> GetAllEmployees()
        {
            using(PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var employees = from emp in context.Employees
                                join d in context.Departments on emp.DepartmentId equals d.Id
                                join p in context.Positions on emp.PositionId equals p.Id
                                where(emp.IsDeleted == false)
                                select new EmployeeDetailsDto
                                {
                                    EmployeeId= emp.Id,
                                    EmployeeName = emp.Name,
                                    DepartmentName = d.Name,
                                    PositionName = p.Name
                                };
                return employees.ToList();
            }
        }

        public async void Delete(int employeeId, string modifiedByName)
        {
            using(PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var employee = await context.Employees.FindAsync(employeeId);
                if (employee != null)
                {
                    employee.IsDeleted = true;
                    employee.ModifiedByName = modifiedByName;
                    employee.ModifiedDate = DateTime.Now;

                    context.Employees.Update(employee);
                    context.SaveChanges();
                }
            }
        }

        public async Task<EmployeeDetailsDto> GetById(int employeeId)
        {
            using (PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var employee = await context.Employees
                    .Where(e => e.Id == employeeId)
                    .Select(e => new EmployeeDetailsDto
                    {
                        EmployeeId = e.Id
                    })
                    .FirstOrDefaultAsync();

                return employee;
            }
        }
    }
}
