using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonnelManagerContext _context;
        private EfShiftTypeRepository? _shiftTypeRepository;

        public UnitOfWork(PersonnelManagerContext context)
        {
            _context = context;
        }

        public IShiftTypeRepository ShiftTypes => throw new NotImplementedException();

        public IEmployeeRepository Employees => throw new NotImplementedException();

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
