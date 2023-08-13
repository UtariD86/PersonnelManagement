using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Concrete.EntityFramework;
using static System.Formats.Asn1.AsnWriter;

namespace PersonnelManagement.Data.Concrete.Repositories
{
    public class EfDepartmentRepository : EfEntityRepositoryBase<Department>, IDepartmentRepository
    {
        private readonly PersonnelManagerContext context;
        private readonly IUnitOfWork unitOfWork;
        private readonly ServiceProvider serviceProvider;

        public EfDepartmentRepository(PersonnelManagerContext _context, IUnitOfWork unitOfWork) : base(_context)
        {
            context = _context;
            this.unitOfWork = unitOfWork;
        }
    }
}
