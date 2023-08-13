using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Mvc.Models
{
    public class EmployeeModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Name { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Surname { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string? IdentityNumber { get; set; }

        public int? GenderId { get; set; }

        public Gender? Gender { get; set; }

        public List<KeyValuePair<int, string>>? Genders { get; set; }

        public int? InsuranceId { get; set; }

        public Insurances? InsuranceType { get; set; }

        public List<KeyValuePair<int, string>>? InsuranceTypes { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? StartOfWork { get; set; }

        public DateTime? EndOfWork { get; set; }

        public string? EndReason { get; set; }

        public DateTime? InsuranceStartDate { get; set; }

        public string? NetSalary { get; set; }

        public string? GrossSalary { get; set; }

        public string? Adress { get; set; }

        public int? BloodTypeId { get; set; }

        public BloodType BloodType { get; set; }

        public List<KeyValuePair<int, string>>? BloodTypes { get; set; }

        public int? DepartmentId { get; set; }

        [Required]
        public string? DepartmentName { get; set; }

        public List<Department>? Departments { get; set; }

        public int? PositionId { get; set; }

        [Required]
        public string? PositionName { get; set; }

        public List<Position>? Positions { get; set; }
        [Required]
        public string? ModifiedByName { get; set; }
    }
}
