using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Entities.Abstract;

namespace PersonnelManagement.Entities.Concrete
{
    public class RequestStatus : EntityBase, IEntity
    {
        [Key]
        public override int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
