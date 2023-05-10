using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Concrete.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Position)
                   .WithMany()
                   .HasForeignKey(e => e.PositionId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(d => d.Positions)
                   .WithOne(p => p.Department)
                   .HasForeignKey(p => p.DepartmentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ShiftMap : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.HasOne(s => s.Employee)
                   .WithMany(e => e.Shifts)
                   .HasForeignKey(s => s.EmployeeId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.ShiftType)
                   .WithMany(st => st.Shifts)
                   .HasForeignKey(s => s.ShiftTypeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ScheduleShiftMap : IEntityTypeConfiguration<ScheduleShift>
    {
        public void Configure(EntityTypeBuilder<ScheduleShift> builder)
        {
            builder.HasOne(ss => ss.Shift)
                   .WithMany(s => s.ScheduleShifts)
                   .HasForeignKey(ss => ss.ShiftId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

    //public class RequestMap : IEntityTypeConfiguration<Request>
    //{
    //    public void Configure(EntityTypeBuilder<Request> builder)
    //    {
    //        builder.HasOne(r => r.RequestStatus)
    //               .WithMany(rs => rs.Requests)
    //               .HasForeignKey(r => r.RequestStatusId)
    //               .OnDelete(DeleteBehavior.NoAction);

    //        builder.HasOne(r => r.Shift)
    //               .WithMany(rs => rs.Requests)
    //               .HasForeignKey(r => r.ShiftId)
    //               .OnDelete(DeleteBehavior.NoAction);
    //    }
    //}
}
