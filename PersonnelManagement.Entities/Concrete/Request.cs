using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class Request : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public int EmployeeId { get; set; }
        public int ShiftId { get; set; }
        public int RequestStatusId { get; set; }
        public string? Note { get; set; }
        //public Employee? Employee { get; set; }
        public Shift? Shift { get; set; }
        public RequestStatus? RequestStatus { get; set; }
    }
}
