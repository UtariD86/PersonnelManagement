using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zurafworks.Shared.Data.Abstract;

namespace PersonnelManagement.Data.Abstract
{
    public interface IShiftTypeRepository : IEntityRepository<ShiftType>
    {
        Task<ShiftTypeDetailsDto> GetByName(string shiftTypeName);
        public Task<ShiftTypeDetailsDto> GetById(int shiftTypeId);
        List<ShiftTypeDetailsDto> GetAllShiftTypes();
        public Task<ShiftType> Add(ShiftTypeDetailsDto shiftTypeDetailsDto);
        void Delete(int shiftTypeId, string modifiedByName);
        void Update(ShiftTypeDetailsDto shiftTypeDetailsDto);
    }
}
