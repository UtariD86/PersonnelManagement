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
        private readonly PersonnelManagerContext context;

        public EfEmployeeRepository(PersonnelManagerContext _context) : base(_context)
        {
            context = _context;
        }

        public void Add(EmployeeDetailsDto employeeDetailsDto)
        {
            //using(PersonnelManagerContext context = new PersonnelManagerContext())
            //{
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
            //}
        }

        public List<EmployeeDetailsDto> GetAllEmployees()
        {
            //using(PersonnelManagerContext context = new PersonnelManagerContext())
            //{
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
            //}
        }

        public async void Delete(/*int employeeId, string modifiedByName*/EmployeeDetailsDto employeeDetailsDto)
        {
            //using(PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var employee = await context.Employees.FindAsync(employeeDetailsDto.EmployeeId);
                if (employee != null)
                {
                    employee.IsDeleted = true;
                    employee.ModifiedByName = employeeDetailsDto.ModifiedByName;
                    employee.ModifiedDate = DateTime.Now;

                    context.Employees.Update(employee);
                    context.SaveChanges();
                }
            //}
        }

        public async Task<EmployeeUpdateDto> GetById(int employeeId)
        {
            
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var employee = await context.Employees
                    .Where(e => e.Id == employeeId)
                    .Select(e => new EmployeeUpdateDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        DepartmentId= e.DepartmentId,
                        PositionId= e.PositionId,
                    })
                    .FirstOrDefaultAsync();

                return employee;
            //}
        }

        public async void Update(EmployeeUpdateDto employeeUpdateDto)
        {
            //using (PersonnelManagerContext context = new PersonnelManagerContext())
            //{
                var employee = await context.Employees.FindAsync(employeeUpdateDto.Id);
                if (employee != null)
                {
                    employee.Name= employeeUpdateDto.Name;
                    if(employeeUpdateDto.DepartmentId != null) 
                    { 
                        employee.DepartmentId = (int)employeeUpdateDto.DepartmentId;
                    }
                    if (employeeUpdateDto.PositionId != null)
                    {
                        employee.PositionId = (int)employeeUpdateDto.PositionId;
                    }
                    employee.ModifiedByName = employeeUpdateDto.ModifiedByName;
                    employee.ModifiedDate = DateTime.Now;

                    context.Employees.Update(employee);
                    context.SaveChanges();
                }
            //}
        }
    }
}
