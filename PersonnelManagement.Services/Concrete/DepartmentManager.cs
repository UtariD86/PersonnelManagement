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
            var newDepartment = new Department();

            newDepartment.Name = departmentDetailsDto.DepartmentName;
            newDepartment.IsDeleted = false;
            newDepartment.CreatedByName = "default";//şimdilik
            newDepartment.ModifiedByName = "default";
            newDepartment.CreatedDate = DateTime.Now;

            await _unitOfWork.Departments.AddAsync(newDepartment);
            await _unitOfWork.SaveChangesAsync();
            return new DataResult<DepartmentDetailsDto>(ResultStatus.Success, newDepartment.Name + "Başarıyla Eklendi",departmentDetailsDto);

        }

        public async Task<IResult> Delete(int departmentId, string modifiedByName)
        {
            var department = _unitOfWork.Departments.GetById(departmentId);

            var deletedDep = new Department();

            deletedDep.Id = departmentId;
            deletedDep.IsDeleted = true;
            deletedDep.ModifiedByName = modifiedByName;

            if (department != null)
            {
                //_unitOfWork.Departments.Delete(departmentId, modifiedByName);
                await _unitOfWork.Departments.UpdateAsync(deletedDep);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public async Task<IDataResult<IList<DepartmentDetailsDto>>> GetAll()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync(d => d.IsDeleted == false);
            var depList = new List<DepartmentDetailsDto>();
            for (int i = 0; i < departments.Count; i++)
            {
                var department = new DepartmentDetailsDto();
                department.DepartmentId = departments[i].Id;
                department.DepartmentName = departments[i].Name;
                depList.Add(department);
            }
                //}


                //if (departments.Count > -1)
                //{
                //    return new DataResult<DepartmentListDto>(ResultStatus.Success, new DepartmentListDto
                //    {
                //        Departments = departments,
                //        ResultStatus = ResultStatus.Success

                //    });
                //}
                //return new DataResult<DepartmentListDto>(ResultStatus.Error, "başarılı", new DepartmentListDto
                //{
                //    Departments = null,
                //    ResultStatus = ResultStatus.Error
                //});
                //var departments = _unitOfWork.Departments.GetAllAsync().Result;
                if (departments.Count > -1)
                {
                    return new DataResult<List<DepartmentDetailsDto>>(ResultStatus.Success, depList);
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

                var newDepartment = new Department();

                newDepartment.Id = departmentDetailsDto.DepartmentId;
                newDepartment.Name = departmentDetailsDto.DepartmentName;
                newDepartment.ModifiedByName = departmentDetailsDto.ModifiedByName;

                await _unitOfWork.Departments.UpdateAsync(newDepartment);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili Departman güncellenemedi");
        }
    }
}
