using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class Employee : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Position? Position { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        //public virtual ICollection<Request> Requests { get; set; }
    }
}
