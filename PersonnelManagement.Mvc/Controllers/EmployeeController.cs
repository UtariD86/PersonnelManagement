using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Concrete;
using System.Dynamic;
using zurafworks.Shared.Utilities.Results.ComplexTypes;
using PersonnelManagement.Mvc.Models;
using Newtonsoft.Json;
using PersonnelManagement.Services.Abstract;

namespace PersonnelManagement.Mvc.Controllers
{
    public class EmployeeController : Controller
    {

        //EmployeeManager em = new EmployeeManager();
        //Sayfaları ayrı yap
        //EmployeeManager em;
        //DepartmentManager dm;
        //PositionManager pm;
        private readonly IEmployeeService _em;
        private readonly IDepartmentService _dm;
        private readonly IPositionService _pm;
        public EmployeeController(IEmployeeService em, IDepartmentService dm, IPositionService pm)
        {
            _em = em;
           _dm= dm;
            _pm = pm;
            //IEmployeeRepository employeeRepository = new EfEmployeeRepository(new PersonnelManagerContext());
            //IDepartmentRepository departmentRepository = new EfDepatmentRepository(new PersonnelManagerContext());
            //IPositionRepository positionRepository = new EfPositionRepository(new PersonnelManagerContext());
            //em = new EmployeeManager(employeeRepository, departmentRepository, positionRepository);
            //dm = new DepartmentManager(departmentRepository);
            //pm = new PositionManager(positionRepository, departmentRepository);
            
        }

        
        public IActionResult Index()
        {
            
            var result = _em.GetAll().Result;//seçilen radiobutona göre ya da açılır liste ile  result'a "em" , "dm" ya da daha sonra diğer modellerimi eşitleyeceğim
            var result2 = _dm.GetAll().Result;
            if (result.ResultStatus == ResultStatus.Success)
            {
                dynamic mymodel = new ExpandoObject();

                mymodel.Employees = _em.GetAll().Result.Data;
                mymodel.Departments = _dm.GetAll().Result.Data;
                mymodel.Positions = _pm.GetAll().Result.Data;
                
                return View(mymodel);
                
            }
            return NotFound();
        }

        public JsonResult GetEmployees()
        {
            var data = _em.GetAll().Result.Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult AddEmployees(AddEmployeeModel empModel)
        {
            var newEmp = new EmployeeDetailsDto();

            newEmp.EmployeeName = empModel.NewEmployee;
            newEmp.DepartmentName = empModel.SelectedDepartment;
            newEmp.PositionName = empModel.SelectedPosition;

            _em.Add(newEmp);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteEmployees(DeleteEmployeeModel empModel)
        {
            var deleteEmp = new EmployeeDetailsDto();

            deleteEmp.EmployeeId = empModel.EmployeeId;
            deleteEmp.ModifiedByName = empModel.ModifiedByName;

            _em.Delete(deleteEmp);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateEmployees(UpdateEmployeeModel empModel)
        {
            var newEmp = new EmployeeDetailsDto();

            newEmp.EmployeeId = empModel.EmployeeId;
            newEmp.EmployeeName = empModel.NewEmployee;
            newEmp.DepartmentName = empModel.SelectedDepartment;
            newEmp.PositionName = empModel.SelectedPosition;
            newEmp.ModifiedByName = empModel.ModifiedByName;

            _em.Update(newEmp);

            return RedirectToAction("Index");
        }
    }
}
