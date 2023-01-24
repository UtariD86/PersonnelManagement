using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Abstract;

namespace PersonnelManagement.Data.Abstract
{
    public interface IPositionRepository : IEntityRepository<Position>
    {
        List<PositionDeta> GetAll();
    }
}
