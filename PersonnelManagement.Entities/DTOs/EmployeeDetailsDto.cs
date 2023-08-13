using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? IdentityNumber { get; set; }
        public int? GenderId { get; set; }
        public string? Gender { get; set; }
        public int? InsuranceTypeId { get; set; }
        public string? InsuranceType { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? StartOfWork { get; set; }
        public DateTime? EndOfWork { get; set; }
        public string? EndReason { get; set; }
        public DateTime? InsuranceStartDate { get; set; }
        public string? NetSalary { get; set; }
        public string? GrossSalary { get; set; }
        public string? Adress { get; set; }
        public int? BloodTypeId { get; set; }
        public string? BloodType { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string ModifiedByName { get; set; }
    }
}
