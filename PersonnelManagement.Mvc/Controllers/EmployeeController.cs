using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Concrete;
using System.Dynamic;
using PersonnelManagement.Mvc.Models;
using Newtonsoft.Json;
using PersonnelManagement.Services.Abstract;
using zurafworks.Shared.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;
using PersonnelManagement.Entities.Dtos;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using JsonSerializer = System.Text.Json.JsonSerializer;
using PersonnelManagement.Mvc.Helpers.Abstract;
using PersonnelManagement.Entities.Enums;

namespace PersonnelManagement.Mvc.Controllers
{
    public class EmployeeController : Controller
    {

        //EmployeeManager em = new EmployeeManager();
        //Sayfaları ayrı yap
        //EmployeeManager em;
        //DepartmentManager dm;
        //PositionManager pm;
        private readonly IEmployeeService em;
        private readonly IDepartmentService dm;
        private readonly IPositionService pm;
        private readonly UserManager<User> userManager;
        private readonly IEnumHelper enumHelper;
        public EmployeeController(IEmployeeService _em, IDepartmentService _dm, IPositionService _pm, UserManager<User> _userManager, IEnumHelper _enumHelper)
        {
            em = _em;
            dm= _dm;
            pm = _pm;
            userManager= _userManager;
            enumHelper= _enumHelper;
            //IEmployeeRepository employeeRepository = new EfEmployeeRepository(new PersonnelManagerContext());
            //IDepartmentRepository departmentRepository = new EfDepatmentRepository(new PersonnelManagerContext());
            //IPositionRepository positionRepository = new EfPositionRepository(new PersonnelManagerContext());
            //em = new EmployeeManager(employeeRepository, departmentRepository, positionRepository);
            //dm = new DepartmentManager(departmentRepository);
            //pm = new PositionManager(positionRepository, departmentRepository);
            
        }

        
        public IActionResult Index()
        {
            
            var result = em.GetAll().Result;//seçilen radiobutona göre ya da açılır liste ile  result'a "em" , "dm" ya da daha sonra diğer modellerimi eşitleyeceğim
            var result2 = dm.GetAll().Result;
            if (result.ResultStatus == ResultStatus.Success)
            {
                dynamic mymodel = new ExpandoObject();

                mymodel.Employees = em.GetAll().Result.Data;
                mymodel.Departments = dm.GetAll().Result.Data;
                mymodel.Positions = pm.GetAll().Result.Data;
                
                return View(mymodel);
                
            }
            return NotFound();
        }

        public JsonResult GetEmployees()
        {
            var lastData = em.GetAllWithRelations().Result.Data;
            //var data = em.GetAll().Result.Data;
            //var lastData = new List<EmployeeModel>();

            //for (int i = 0; i < data.Count; i++)
            //{
            //    var employee = new EmployeeModel();
            //    employee.Id = data[i].Id;
            //    employee.Name = data[i].Name;
            //    employee.DepartmentId = data[i].DepartmentId;
            //    employee.DepartmentName = dm.GetById(data[i].DepartmentId).Result.Data.Name;
            //    employee.PositionId = data[i].PositionId;
            //    employee.PositionName = pm.GetById(data[i].PositionId).Result.Data.Name;

            //    lastData.Add(employee);
            //}

            return Json(lastData, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateEmployee(int? id)
        {
            var model = new EmployeeModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            var departments = dm.GetAll().Result.Data;
            var positions = pm.GetAll().Result.Data;
            var genders = enumHelper.GetValuesAndNames<Gender>();
            var bloodTypes = enumHelper.GetValuesAndNames<BloodType>();
            var insuranceTypes = enumHelper.GetValuesAndNames<Insurances>();
            if (id.HasValue)
            {
                var eResult = await em.GetById(id.Value);
                var eEntity = eResult.Data;
                if (eEntity != null && eResult.ResultStatus == ResultStatus.Success)
                {
                    var pResult = await pm.GetById(eEntity.PositionId);
                    var pEntity = pResult.Data;
                    if (pEntity != null && pResult.ResultStatus == ResultStatus.Success)
                    {
                        var dResult = await dm.GetById(eEntity.DepartmentId);
                        var dEntity = dResult.Data;
                        if (dEntity != null && dResult.ResultStatus == ResultStatus.Success)
                        {
                            model = new EmployeeModel
                            {
                                Id = eEntity.Id,
                                Name = eEntity.Name,
                                Surname = eEntity.Surname,
                                IdentityNumber = eEntity.IdentityNumber,
                                GenderId = eEntity.GenderId,
                                InsuranceId = eEntity.InsuranceTypeId,
                                BirthDate = eEntity.BirthDate,
                                StartOfWork = eEntity.StartOfWork,
                                EndOfWork = eEntity.EndOfWork,
                                EndReason = eEntity.EndReason,
                                InsuranceStartDate = eEntity.InsuranceStart,
                                NetSalary = eEntity.NetSalary,
                                GrossSalary = eEntity.GrossSalary,
                                BloodTypeId = eEntity.BloodTypeId,
                                Adress = eEntity.Adress,
                                PositionId = pEntity.Id,
                                PositionName = pEntity.Name,
                                DepartmentId = dEntity.Id,
                                DepartmentName = dEntity.Name,
                                ModifiedByName = user != null ? user.Name : "",
                                
                            };
                        }
                    }
                }
                TempData["OperationType"] = " Guncelleme ";
            }
            else
            {
                TempData["OperationType"] = " Veritabanına Kaydetme ";
            }
            model.Departments = (List<Department>)departments;
            model.Positions = (List<Position>)positions;
            model.Genders = genders;
            model.BloodTypes = bloodTypes;
            model.InsuranceTypes = insuranceTypes;
            return PartialView(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SaveEmployee(EmployeeModel model)
        {
            var message = "resultMessage";
            var id = model.Id;
            var newEmp = new Employee();
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (model.Name != null && model.Name != "")
            {
                newEmp.Name = model.Name;
                newEmp.Surname = model.Surname;
                newEmp.IdentityNumber = model.IdentityNumber;
                newEmp.GenderId = model.GenderId;
                newEmp.InsuranceTypeId = model.InsuranceId;
                newEmp.BirthDate = model.BirthDate;
                newEmp.StartOfWork = model.StartOfWork;
                newEmp.EndOfWork = model.EndOfWork;
                newEmp.EndReason = model.EndReason;
                newEmp.InsuranceStart = model.InsuranceStartDate;
                newEmp.NetSalary = model.NetSalary;
                newEmp.GrossSalary = model.GrossSalary;
                newEmp.Adress = model.Adress;
                newEmp.BloodTypeId = model.BloodTypeId;
                newEmp.ModifiedByName = user != null ? user.Name : "";
                if (id != 0 && id != null)
                {
                    newEmp.Id = id;
                    if (model.DepartmentId != null && model.DepartmentId != 0)
                    {
                        newEmp.DepartmentId = model.DepartmentId.Value;
                    }
                    if (model.PositionId != null && model.PositionId != 0)
                    {
                        newEmp.PositionId = model.PositionId.Value;
                    }

                    var result = em.Update(newEmp);
                    if (!string.IsNullOrEmpty(result.Result.Message))
                    {
                        message = result.Result.Message;
                    }
                }
                if (id == 0)
                {
                    newEmp.CreatedByName = newEmp.ModifiedByName;
                    var result = em.Add(newEmp);
                    if (!string.IsNullOrEmpty(result.Result.Message))
                    {
                        message = result.Result.Message;
                    }
                }
                if (id == null || model.Name == null)
                {
                    message = "Kaydetmeye çalıştığınız veriler kayıp ya da bulunamıyor.";
                }
            }
            return Json(message);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployees(EmployeeModel model)
        {
            var message = "tempMessage";
            if (model.Id != null || model.Id != 0)
            {
                var employee = new Employee();
                employee.Id = model.Id;
                var result = await em.Delete(employee);
                if (!string.IsNullOrEmpty(result.Message))
                {
                    message = result.Message;
                }
            }
            else
            {
                message = "Silinirken bir hata oluştu!";
            }
            return Json(message);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExcelExport()
        {
            var stream = em.ExportToExcel();
            string fileName = "employees" + DateTime.Now.ToString() + ".xlsx";
            try
            {
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = em.GetById(id).Result.Data;
            var department = dm.GetById(employee.DepartmentId).Result.Data;
            var position = pm.GetById(employee.PositionId).Result.Data;
            var lastData = new EmployeeModel();
            lastData.Name = employee.Name;
            lastData.DepartmentName = department.Name;
            lastData.PositionName = position.Name;
            return Json(lastData);
        }
    }
}
