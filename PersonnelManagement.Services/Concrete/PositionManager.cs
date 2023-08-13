using PersonnelManagement.Data.Abstract;
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

namespace PersonnelManagement.Services.Concrete
{
    public class PositionManager : IPositionService
    {
        //private readonly IPositionRepository _positionRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PositionManager(/*IPositionRepository positionRepository, IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            //_positionRepository = positionRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork= unitOfWork;
        }

        public async Task<IDataResult<Position>> Add(Position position)
        {
            //var department = await _unitOfWork.Departments.GetAsync(d => d.Name == positionDetailsDto.DepartmentName);
            var _position = await _unitOfWork.Positions.GetAsync(p => p.Name == position.Name);

            //if (department == null)
            //{
            //    return new DataResult<PositionDetailsDto>(ResultStatus.Error, "Seçili Departman Bulunamadı", null);
            //}

            if (_position != null)
            {
                return new DataResult<Position>(ResultStatus.Error, "Pozisyon Halihazırda Mevcut", null);
            }

            //var newPosition = new PositionDetailsDto()
            //{
            //    //DepartmentName = department.DepartmentName,
            //    PositionName = positionDetailsDto.PositionName
            //};

            await _unitOfWork.Positions.AddAsync(position);
            await _unitOfWork.SaveChangesAsync();
            return new DataResult<Position>(ResultStatus.Success, position.Name + "Başarıyla Eklendi", position);

        }

        public async Task<IResult> Delete(Position position)
        {
            var _position =  _unitOfWork.Positions.Get(position.Id);



            if (_position != null)
            {
                _position.IsDeleted = true;
                _position.ModifiedByName = position.ModifiedByName;
                _position.ModifiedDate = DateTime.Now;
                await _unitOfWork.Positions.UpdateAsync(_position);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{position.Name} Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili çalışan bulunamadı");
        }

        public async Task<IDataResult<IList<Position>>> GetAll()
        {
            //var positions = _unitOfWork.Position.GetAllPositions();
            var positions = await _unitOfWork.Positions.GetAllAsync(p => p.IsDeleted == false);
            if (positions.Count > -1)
            {
                return new DataResult<IList<Position>>(ResultStatus.Success, positions);
            }
            return new DataResult<IList<Position>>(ResultStatus.Error, "Hiç Pozisyon bulunamadı", null);
        }

        public async Task<IDataResult<IList<PositionDetailsDto>>> GetAllWithRelations()
        {
            var positions =  _unitOfWork.Positions.GetAllPositions();
            if (positions.Count > -1)
            {
                return new DataResult<IList<PositionDetailsDto>>(ResultStatus.Success, positions);
            }
            return new DataResult<IList<PositionDetailsDto>>(ResultStatus.Error, "Hiç Pozisyon bulunamadı", null);
        }

        public async Task<IDataResult<Position>> GetById(int id)
        {
            var position = _unitOfWork.Positions.Get(id);

            if (position != null)
            {
                return new DataResult<Position>(ResultStatus.Success, position);//dto to entity yüzünden hata çıktı
            }
            return new DataResult<Position>(ResultStatus.Error, "Pozisyon bulunamadı", null);
        }

        public async Task<IResult> Update(Position position)
        {
            if (position != null && position.Id != 0)
            {
                await _unitOfWork.Positions.UpdateAsync(position);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{position.Name} isimli pozisyon başarıyla güncellendi.");
            }
            return new Result(ResultStatus.Error, $"Malesef bir sorun oluştu...", new ArgumentNullException());
        }

        //public async Task<IResult> Update(PositionDetailsDto positionDetailsDto)
        //{
        //    var department = 1;// await _unitOfWork.Departments.GetByName(positionDetailsDto?.DepartmentName);

        //    var position = _unitOfWork.Positions.GetById(positionDetailsDto.PositionId);

        //    if (positionDetailsDto.PositionName == null)
        //    {
        //        positionDetailsDto.PositionName = position.Result.PositionName;
        //    }

        //    if (position != null)
        //    {

        //        var newPosition = new PositionUpdateDto();

        //        newPosition.Id = positionDetailsDto.PositionId;
        //        newPosition.Name = positionDetailsDto.PositionName;
        //        if (department != null)
        //        {
        //            //newPosition.DepartmentId = department.DepartmentId;
        //        }
        //        else
        //        {
        //            newPosition.DepartmentId = null;
        //        }

        //        newPosition.ModifiedByName = positionDetailsDto.ModifiedByName;

        //        _unitOfWork.Positions.Update(newPosition);
        //        return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
        //    }
        //    return new Result(ResultStatus.Error, "Seçili pozisyon güncellenemedi");
        //}
    }
}
