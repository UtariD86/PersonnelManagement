using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Concrete;
using zurafworks.Shared.Utilities.Results.ComplexTypes;

namespace PersonnelManagement.Mvc.Controllers
{
    public class Employee : Controller
    {
        //EmployeeManager em = new EmployeeManager();
        EmployeeManager em;
        DepartmentManager dm;
        public Employee()
        {
            IEmployeeRepository employeeRepository = new EfEmployeeRepository(new PersonnelManagerContext());
            IDepartmentRepository departmentRepository = new EfDepatmentRepository(new PersonnelManagerContext());
            em = new EmployeeManager(employeeRepository);
            dm = new DepartmentManager(departmentRepository);
            
        }
        public IActionResult Index()
        {

            var result = em.GetAll().Result;//seçilen radiobutona göre ya da açılır liste ile  result'a "em" , "dm" ya da daha sonra diğer modellerimi eşitleyeceğim
            if (result.ResultStatus == ResultStatus.Success)
            {
                var values = result.Data;
                return View(values);
            }
            return NotFound();

            //var values = em.GetAll();
            //return View(values);
        }
    }
}
