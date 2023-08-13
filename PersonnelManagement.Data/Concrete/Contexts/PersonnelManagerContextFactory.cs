using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

    namespace PersonnelManagement.Data.Concrete.Contexts
{
    
        public class PersonnelManagementContextFactory : IDesignTimeDbContextFactory<PersonnelManagerContext>
        {
            public PersonnelManagerContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<PersonnelManagerContext>();
                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=PersonnelManager;Integrated Security=true");

                return new PersonnelManagerContext(optionsBuilder.Options);
            }
        }
}
