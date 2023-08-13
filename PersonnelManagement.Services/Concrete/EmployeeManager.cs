using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Results.Abstract;
using zurafworks.Shared.Results.ComplexTypes;
using zurafworks.Shared.Results.Concrete;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace PersonnelManagement.Services.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        //IEmployeeRepository _employeeRepository;
        //IDepartmentRepository _departmentRepository;
        //IPositionRepository _positionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeManager(IUnitOfWork unitOfWork/*IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository*/)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            //_positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Employee>> Add(Employee employee)
        {
            //isme göre departman ve pozisyon getir fonksiyona dtodan gelen departman ve pozisyon adını gönder ama önce getbyname yazmalsıın her biri için
            //var department = await _unitOfWork.Departments.Ge(employee);
            //var position = await _unitOfWork.Positions.GetAsync(d => d.Name == "1");

            //if (department == null)
            //{
            //    return new DataResult<EmployeeDetailsDto>(ResultStatus.Error, "Seçili Departman Bulunamadı", null);
            //}

            //if (position == null)
            //{
            //    return new DataResult<EmployeeDetailsDto>(ResultStatus.Error, "Seçili Pozisyon Bulunamadı", null);
            //}

            //var employee = new EmployeeDetailsDto()
            //{
            //    EmployeeName = employeeDetailsDto.EmployeeName,
            //    //DepartmentName = department.DepartmentName,
            //    //PositionName = position.PositionName
            //};

            if (employee.Name != null && employee.Name!= "" && employee.DepartmentId != null && employee.DepartmentId != 0 && employee.PositionId != null && employee.PositionId != 0)
            {
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();
                return new DataResult<Employee>(ResultStatus.Success, employee.Name + "Başarıyla Eklendi", employee);
            }
            return new DataResult<Employee>(ResultStatus.Error, employee.Name + "Eksik veya yanlış veriler sebebiyle eklenemedi!", null);
        }

        public async Task<IResult> Delete(Employee employee)
        {
            var _employee = await _unitOfWork.Employees.GetAsync(e => e.Id == employee.Id);

            
            if (_employee != null)
            {
                _employee.IsDeleted = true;
                _employee.ModifiedByName = employee.ModifiedByName;
                _employee.ModifiedDate = DateTime.Now;
                await _unitOfWork.Employees.UpdateAsync(_employee);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public MemoryStream ExportToExcel()
        {
            var employees = _unitOfWork.Employees.GetAllEmployees();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");

                // Başlıklar
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Surname";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Position";
                worksheet.Cell(1, 6).Value = "Identity Number";
                worksheet.Cell(1, 7).Value = "Birth Date";
                worksheet.Cell(1, 8).Value = "Start of Work";
                worksheet.Cell(1, 9).Value = "End of Work";
                worksheet.Cell(1, 10).Value = "End Reason";
                worksheet.Cell(1, 11).Value = "Insurance Start";
                worksheet.Cell(1, 12).Value = "Net Salary";
                worksheet.Cell(1, 13).Value = "Gross Salary";
                worksheet.Cell(1, 14).Value = "Address";
                worksheet.Cell(1, 15).Value = "Gender";
                worksheet.Cell(1, 16).Value = "Insurance Type";
                worksheet.Cell(1, 17).Value = "Blood Type";

                // Veriler
                for (int i = 0; i < employees.Count; i++)
                {
                    var e = employees[i];
                    worksheet.Cell(i + 2, 1).Value = e.Id;
                    worksheet.Cell(i + 2, 2).Value = e.Name;
                    worksheet.Cell(i + 2, 3).Value = e.Surname;
                    worksheet.Cell(i + 2, 4).Value = e.DepartmentName;
                    worksheet.Cell(i + 2, 5).Value = e.PositionName;
                    worksheet.Cell(i + 2, 6).Value = e.IdentityNumber;
                    worksheet.Cell(i + 2, 7).Value = e.BirthDate;
                    worksheet.Cell(i + 2, 8).Value = e.StartOfWork;
                    worksheet.Cell(i + 2, 9).Value = e.EndOfWork;
                    worksheet.Cell(i + 2, 10).Value = e.EndReason;
                    worksheet.Cell(i + 2, 11).Value = e.InsuranceStartDate;
                    worksheet.Cell(i + 2, 12).Value = e.NetSalary;
                    worksheet.Cell(i + 2, 13).Value = e.GrossSalary;
                    worksheet.Cell(i + 2, 14).Value = e.Adress;
                    worksheet.Cell(i + 2, 15).Value = e.Gender;
                    worksheet.Cell(i + 2, 16).Value = e.InsuranceType;
                    worksheet.Cell(i + 2, 17).Value = e.BloodType;
                }

                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return stream;
            }
        }

        public async Task<IDataResult<IList<Employee>>> GetAll()
        {
            var employees =  await _unitOfWork.Employees.GetAllAsync(e => e.IsDeleted == false);
            if (employees.Count > -1)
            {
                return new DataResult<IList<Employee>>(ResultStatus.Success, employees);
            }
            return new DataResult<IList<Employee>>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
            
        }

        public async Task<IDataResult<IList<EmployeeDetailsDto>>> GetAllWithRelations()
        {
            var employees = _unitOfWork.Employees.GetAllEmployees();
            if (employees.Count > -1)
            {
                return new DataResult<IList<EmployeeDetailsDto>>(ResultStatus.Success, employees);
            }
            return new DataResult<IList<EmployeeDetailsDto>>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
        }

        public async Task<IDataResult<Employee>> GetById(int id)
        {
            var employee = _unitOfWork.Employees.Get(id);

            if (employee != null)
            {
                return new DataResult<Employee>(ResultStatus.Success, employee);//dto to entity yüzünden hata çıktı
            }
            return new DataResult<Employee>(ResultStatus.Error, "Çalışan bulunamadı", null);
        }

        public async Task<IResult> Update(Employee employee)
        {
            if (employee != null && employee.Id != 0)
            {
                await _unitOfWork.Employees.UpdateAsync(employee);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{employee.Name} isimli pozisyon başarıyla güncellendi.");
            }
            return new Result(ResultStatus.Error, $"Malesef bir sorun oluştu...", new ArgumentNullException());
        }

    }
}
