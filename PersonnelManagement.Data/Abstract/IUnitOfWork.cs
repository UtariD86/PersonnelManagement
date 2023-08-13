using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Abstract
{
    public interface IUnitOfWork //: //IAsyncDisposable
    {
        IShiftTypeRepository ShiftTypes { get; }
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        IPositionRepository Positions { get; }
        IScheduleShiftRepository ScheduleShifts { get; }
        IShiftRepository Shifts { get; }
        Task<int> SaveChangesAsync();
    }
}
