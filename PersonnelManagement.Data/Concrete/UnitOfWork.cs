using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        //private readonly IDbContextFactory<PersonnelManagerContext> _contextFactory;
        private readonly PersonnelManagerContext _context;
        private EfShiftTypeRepository? _shiftTypeRepository;
        private EfEmployeeRepository? _employeeRepository;
        private EfDepartmentRepository? _departmentRepository;
        private EfPositionRepository? _positionRepository;
        private EfScheduleShiftRepository? _scheduleShiftRepository;
        private EfShiftRepository? _shiftRepository;
        public UnitOfWork(PersonnelManagerContext context /*IDbContextFactory<PersonnelManagerContext> contextFactory*/)
        {
            _context = context;
            //_contextFactory = contextFactory;
        }

        public IShiftTypeRepository ShiftTypes => _shiftTypeRepository ?? new EfShiftTypeRepository(_context);

        public IEmployeeRepository Employees => _employeeRepository?? new EfEmployeeRepository(_context);

        public IDepartmentRepository Departments => _departmentRepository ?? new EfDepartmentRepository(_context, this);

        public IPositionRepository Positions => _positionRepository?? new EfPositionRepository(_context);

        public IScheduleShiftRepository ScheduleShifts => _scheduleShiftRepository?? new EfScheduleShiftRepository(_context);

        public IShiftRepository Shifts => _shiftRepository?? new EfShiftRepository(_context);

        //public async ValueTask DisposeAsync()
        //{
        //    await _context.DisposeAsync();
        //}

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata işleme kodlarını buraya ekleyin
                // ex hatası üzerinde çalışabilirsiniz veya loglama yapabilirsiniz
                throw; // İstisnayı yeniden fırlatmak veya alternatif bir işlem yapmak isterseniz throw ile fırlatabilirsiniz.
            }
        }
    }
}
