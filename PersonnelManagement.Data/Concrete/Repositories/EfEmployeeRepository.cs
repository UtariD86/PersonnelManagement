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

        public void Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeDetailsDto> GetAllEmployees()
        {
            using(PersonnelManagerContext context = new PersonnelManagerContext())
            {
                var employees = from emp in context.Employees
                                join d in context.Departments on emp.DepartmentId equals d.Id
                                join p in context.Positions on emp.PositionId equals p.Id
                                select new EmployeeDetailsDto
                                {
                                    EmployeeName = emp.Name,
                                    DepartmentName = d.Name,
                                    PositionName = p.Name
                                };
                return employees.ToList();
            }
        }
    }
}
