using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Concrete;
using System.Dynamic;
using zurafworks.Shared.Utilities.Results.ComplexTypes;

namespace PersonnelManagement.Mvc.Controllers
{
    public class Employee : Controller
    {
        //EmployeeManager em = new EmployeeManager();
        EmployeeManager em;
        DepartmentManager dm;
        PositionManager pm;
        public Employee()
        {
            IEmployeeRepository employeeRepository = new EfEmployeeRepository(new PersonnelManagerContext());
            IDepartmentRepository departmentRepository = new EfDepatmentRepository(new PersonnelManagerContext());
            IPositionRepository positionRepository = new EfPositionRepository(new PersonnelManagerContext());
            em = new EmployeeManager(employeeRepository);
            dm = new DepartmentManager(departmentRepository);
            pm = new PositionManager(positionRepository);
            
        }
        public IActionResult Index()
        {
            
            var result = em.GetAll().Result;//seçilen radiobutona göre ya da açılır liste ile  result'a "em" , "dm" ya da daha sonra diğer modellerimi eşitleyeceğim
            var result2 = dm.GetAll().Result;
            if (result.ResultStatus == ResultStatus.Success)
            {
                dynamic mymodel = new ExpandoObject();
                //dynamic posmodel = new ExpandoObject();
                mymodel.Employees = em.GetAll().Result.Data;
                mymodel.Departments = dm.GetAll().Result.Data;
                mymodel.Positions = pm.GetAll().Result.Data;
                //posmodel = pm.GetAll().Result.Data;
                //mymodel.FiltredPositions = pm.GetAll().Result.Data.Where(x => x.DepartmentName == mymodel.Departments.DepartmentName).ToList();

                //var values = mymodel.Data;
                return View(mymodel/*, posmodel*/);
            }
            return NotFound();

            
            

            //var values = em.GetAll();
            //return View(values);
        }
    }
}
