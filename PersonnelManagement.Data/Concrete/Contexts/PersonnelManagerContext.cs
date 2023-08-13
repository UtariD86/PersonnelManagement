using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Concrete.Mappings;
using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Concrete.Contexts
{
    public class PersonnelManagerContext : IdentityDbContext<User,Role,int,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {

        public PersonnelManagerContext(DbContextOptions<PersonnelManagerContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PersonnelManager;Integrated Security=true");
        //    }
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PersonnelManager;Integrated Security=true");
        //}

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        //public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleShift> ScheduleShifts { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftType> ShiftTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Employee>() //Employee-Department
            //    .HasOne(e => e.Department)
            //    .WithMany(d => d.Employees)
            //    .HasForeignKey(e => e.DepartmentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Employee>() //Employee-Position
            //    .HasOne(e => e.Position)
            //    .WithMany()//tekrar bak
            //    .HasForeignKey(e => e.PositionId)
            //    .OnDelete(DeleteBehavior.NoAction);


            //modelBuilder.Entity<Department>() //Department-Position
            //    .HasMany(d => d.Positions)
            //    .WithOne(p => p.Department)
            //    .HasForeignKey(p => p.DepartmentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Shift>() //Shift-Employee
            //    .HasOne(s => s.Employee)
            //    .WithMany(e => e.Shifts)
            //    .HasForeignKey(s => s.EmployeeId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Shift>() //Shift-ShiftType
            //    .HasOne(s => s.ShiftType)
            //    .WithMany(st => st.Shifts)
            //    .HasForeignKey(s => s.ShiftTypeId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<ScheduleShift>() //ScheduleShift-Shift
            //    .HasOne(ss => ss.Shift)
            //    .WithMany(s => s.ScheduleShifts)
            //    .HasForeignKey(ss => ss.ShiftId)
            //    .OnDelete(DeleteBehavior.NoAction);

            ////modelBuilder.Entity<ScheduleShift>() //ScheduleShift-Schedule
            ////    .HasOne(ss => ss.Schedule)
            ////    .WithMany(s => s.ScheduleShifts)
            ////    .HasForeignKey(ss => ss.ScheduleId)
            ////    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Request>() //Request-RequestStatus
            //    .HasOne(r => r.RequestStatus)
            //    .WithMany(rs => rs.Requests)
            //    .HasForeignKey(r => r.RequestStatusId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Request>() //Request-Shift
            //   .HasOne(r => r.Shift)
            //   .WithMany(rs => rs.Requests)
            //   .HasForeignKey(r => r.ShiftId)
            //   .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new DepartmentMap());
            modelBuilder.ApplyConfiguration(new ShiftMap());
            modelBuilder.ApplyConfiguration(new ScheduleShiftMap());

            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());


        }
    }
}
