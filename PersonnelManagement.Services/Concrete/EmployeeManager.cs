using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;
using zurafworks.Shared.Utilities.Results.ComplexTypes;
using zurafworks.Shared.Utilities.Results.Concrete;

namespace PersonnelManagement.Services.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        IDepartmentRepository _departmentRepository;
        IPositionRepository _positionRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
        }

        public async Task<IDataResult<EmployeeDetailsDto>> Add(EmployeeDetailsDto employeeDetailsDto)
        {
            //isme göre departman ve pozisyon getir fonksiyona dtodan gelen departman ve pozisyon adını gönder ama önce getbyname yazmalsıın her biri için
            var department = await _departmentRepository.GetByName(employeeDetailsDto?.DepartmentName);
            var position = await _positionRepository.GetByName(employeeDetailsDto?.PositionName);

            if (department == null)
            {
                return new DataResult<EmployeeDetailsDto>(ResultStatus.Error, "Seçili Departman Bulunamadı", null);
            }

            if (position == null)
            {
                return new DataResult<EmployeeDetailsDto>(ResultStatus.Error, "Seçili Pozisyon Bulunamadı", null);
            }

            var employee = new EmployeeDetailsDto()
            {
                EmployeeName = employeeDetailsDto.EmployeeName,
                DepartmentName = department.DepartmentName,
                PositionName = position.PositionName
            };

            _employeeRepository.Add(employee);

            return new DataResult<EmployeeDetailsDto>(ResultStatus.Success, employee.EmployeeName + "Başarıyla Eklendi", employee);
            //her biri için null ise hata döndür değilse isim idler ve createdbyname i eşitle ve ekleme fonksiyonunu çağır. succes döndür

        }

        public async Task<IResult> Delete(int employeeId, string modifiedByName)
        {
            var employee = _employeeRepository.GetById(employeeId);

            
            if (employee != null)
            {
                _employeeRepository.Delete(employeeId, modifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public async Task<IDataResult<List<EmployeeDetailsDto>>> GetAll()
        {
            var employees = _employeeRepository.GetAllEmployees();
            if (employees.Count > -1)
            {
                return new DataResult<List<EmployeeDetailsDto>>(ResultStatus.Success, employees);
            }
            return new DataResult<List<EmployeeDetailsDto>>(ResultStatus.Error, "Hiç çalışan bulunamadı", null);
            
        }
    }
}
