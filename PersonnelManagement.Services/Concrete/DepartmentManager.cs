using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Services.Abstract;
using PersonnelManagement.Entities.Concrete;
using zurafworks.Shared.Results.Abstract;
using zurafworks.Shared.Results.Concrete;
using zurafworks.Shared.Results.ComplexTypes;
using zurafworks.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IDataResult<Department>> Add(Department department)
        {
            var checkDepartment = await _unitOfWork.Departments.GetAsync(d => d.Name == department.Name);

            if (checkDepartment != null)
            {
                return new DataResult<Department>(ResultStatus.Error, "Departman Halihazırda Mevcut", null);
            }

            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();
            return new DataResult<Department>(ResultStatus.Success, department.Name + "Başarıyla Eklendi",department);

        }

        public async Task<IResult> Delete(Department department)
        {
            var _department = _unitOfWork.Departments.Get(department.Id);

            

            if (_department != null)
            {
                _department.IsDeleted = true;
                _department.ModifiedByName = department.ModifiedByName;
                _department.ModifiedDate = DateTime.Now;
                //_unitOfWork.Departments.Delete(departmentId, modifiedByName);
                await _unitOfWork.Departments.UpdateAsync(_department);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{department.Name} Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public async Task<IDataResult<IList<Department>>> GetAll()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync(d => d.IsDeleted == false);

            if (departments.Count > -1)
            {
                return new DataResult<IList<Department>>(ResultStatus.Success, departments);
            }
            return new DataResult<IList<Department>>(ResultStatus.Error, "Hiç Departman bulunamadı", null);
        }

        public async Task<IDataResult<Department>> GetById(int id)
        {
            var department = _unitOfWork.Departments.Get(id);

            if (department != null)
            {
                return new DataResult<Department>(ResultStatus.Success, department);//dto to entity yüzünden hata çıktı
            }
            return new DataResult<Department>(ResultStatus.Error, "Departman bulunamadı", null);
        }

        public async Task<IResult> Update(Department department)
        {
            if (department != null && department.Id != 0)
            {
                await _unitOfWork.Departments.UpdateAsync(department);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{department.Name} isimli departman başarıyla güncellendi.");
            }
            return new Result(ResultStatus.Error, $"Malesef bir sorun oluştu...", new ArgumentNullException());
        }
    }
}
