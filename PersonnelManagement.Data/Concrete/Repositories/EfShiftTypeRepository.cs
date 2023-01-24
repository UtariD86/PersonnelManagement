using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Concrete.EntityFramework;

namespace PersonnelManagement.Data.Concrete.Repositories
{
    public class EfShiftTypeRepository : EfEntityRepositoryBase<ShiftType>, IShiftTypeRepository
    {
        public EfShiftTypeRepository(DbContext context) : base(context) 
        {
        }
    }
}
