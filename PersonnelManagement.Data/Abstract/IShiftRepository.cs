﻿using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Abstract
{
    public interface IShiftRepository
    {
        public Task<ShiftDetailsDto> GetById(int shiftId);
        List<ShiftDetailsDto> GetAllShifts();
        public Task<Shift> Add(ShiftDetailsDto shiftDetailsDto);
        void Delete(int shiftId, string modifiedByName);
        public Task<int> SaveAsync();
        //void Update(DepartmentUpdateDto departmentUpdateDto);
    }
}
