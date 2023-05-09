using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Utilities.Results.Abstract;
using zurafworks.Shared.Utilities.Results.Concrete;
using zurafworks.Shared.Utilities.Results.ComplexTypes;
using PersonnelManagement.Entities.Concrete;

namespace PersonnelManagement.Services.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        //IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentManager(IUnitOfWork unitOfWork)//IDepartmentRepository departmentRepository)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }

        public async Task<IDataResult<DepartmentDetailsDto>> Add(DepartmentDetailsDto departmentDetailsDto)
        {
            var checkDepartment = await _unitOfWork.Departments.GetByName(departmentDetailsDto?.DepartmentName);

            if (checkDepartment != null)
            {
                return new DataResult<DepartmentDetailsDto>(ResultStatus.Error, "Departman Halihazırda Mevcut", null);
            }
            var newDepartment = new DepartmentDetailsDto()
            {
                DepartmentName = departmentDetailsDto.DepartmentName
            };

            _unitOfWork.Departments.Add(newDepartment);

            return new DataResult<DepartmentDetailsDto>(ResultStatus.Success, newDepartment.DepartmentName + "Başarıyla Eklendi",newDepartment);

        }

        public async Task<IResult> Delete(int departmentId, string modifiedByName)
        {
            var department = _unitOfWork.Departments.GetById(departmentId);


            if (department != null)
            {
                _unitOfWork.Departments.Delete(departmentId, modifiedByName);
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public async Task<IDataResult<List<DepartmentDetailsDto>>> GetAll()
        {
            var departments = _unitOfWork.Departments.GetAllDepartments();
            if (departments.Count >- 1)
            {
                return new DataResult<List<DepartmentDetailsDto>>(ResultStatus.Success, departments);
            }
            return new DataResult<List<DepartmentDetailsDto>>(ResultStatus.Error, "Hiç Departman bulunamadı", null);

            
        }

        public async Task<IDataResult<DepartmentDetailsDto>> GetByName(string departmentName)
        {
            var department = await _unitOfWork.Departments.GetByName(departmentName);
            if (department != null)
            {
                return new DataResult<DepartmentDetailsDto>(ResultStatus.Success, department);
            }
            return new DataResult<DepartmentDetailsDto>(ResultStatus.Error, "Departman bulunamadı", null);

        }

        public async Task<IResult> Update(DepartmentDetailsDto departmentDetailsDto)
        {

            var department = _unitOfWork.Departments.GetById(departmentDetailsDto.DepartmentId);

            if (departmentDetailsDto.DepartmentName == null)
            {
                departmentDetailsDto.DepartmentName = department.Result.DepartmentName;
            }



            if (department != null)
            {

                var newDepartment = new DepartmentUpdateDto();

                newDepartment.Id = departmentDetailsDto.DepartmentId;
                newDepartment.Name = departmentDetailsDto.DepartmentName;
                newDepartment.ModifiedByName = departmentDetailsDto.ModifiedByName;

                _unitOfWork.Departments.Update(newDepartment);
                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili Departman güncellenemedi");
        }
    }
}
