using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Entities.Enums;
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



        public List<EmployeeDetailsDto> GetAllEmployees()
        {
            var query = from emp in context.Employees
                        join d in context.Departments on emp.DepartmentId equals d.Id
                        join p in context.Positions on emp.PositionId equals p.Id
                        where (emp.IsDeleted == false && d.IsDeleted == false && p.IsDeleted == false)
                        select new
                        {
                            emp.Id,
                            emp.Name,
                            emp.Surname,
                            DepartmentName = d.Name,
                            PositionName = p.Name,
                            emp.IdentityNumber,
                            emp.BirthDate,
                            emp.StartOfWork,
                            emp.EndOfWork,
                            emp.EndReason,
                            emp.InsuranceStart,
                            emp.NetSalary,
                            emp.GrossSalary,
                            emp.Adress,
                            emp.GenderId,
                            emp.InsuranceTypeId,
                            emp.BloodTypeId
                        };

            var employees = query.ToList();

            return employees.Select(e => new EmployeeDetailsDto
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname,
                DepartmentName = e.DepartmentName,
                PositionName = e.PositionName,
                IdentityNumber = e.IdentityNumber,
                BirthDate = e.BirthDate,
                StartOfWork = e.StartOfWork,
                EndOfWork = e.EndOfWork,
                EndReason = e.EndReason,
                InsuranceStartDate = e.InsuranceStart,
                NetSalary = e.NetSalary,
                GrossSalary = e.GrossSalary,
                Adress = e.Adress,
                Gender = e.GenderId.HasValue ? Enum.GetName(typeof(Gender), e.GenderId.Value) : "Unknown",
                InsuranceType = e.InsuranceTypeId.HasValue ? Enum.GetName(typeof(Insurances), e.InsuranceTypeId.Value) : "Unknown",
                BloodType = e.BloodTypeId.HasValue ? Enum.GetName(typeof(BloodType), e.BloodTypeId.Value) : "Unknown"
            }).ToList();
        }


    }
}
