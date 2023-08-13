using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelManagement.Entities.Concrete;
using zurafworks.Shared.Results.Abstract;

namespace PersonnelManagement.Entities.Dtos
{
    public class UserListDto : DtoGetBase
    {
        public IList<User> Users { get; set; }
    }
}