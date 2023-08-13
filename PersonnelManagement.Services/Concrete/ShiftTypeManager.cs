using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
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


    public class ShiftTypeManager : IShiftTypeService
    {
        //IShiftTypeRepository _shiftTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ShiftTypeManager(IUnitOfWork unitOfWork)
        {
            //_shiftTypeRepository = shiftTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<ShiftType>> Add(ShiftType shiftType)
        {
            var checkSt = await _unitOfWork.ShiftTypes.GetAsync(st => st.Name == shiftType.Name);

            if (checkSt == null)
            {
                await _unitOfWork.ShiftTypes.AddAsync(shiftType);
                await _unitOfWork.SaveChangesAsync();
                return new DataResult<ShiftType>(ResultStatus.Success, shiftType.Name + "Başarıyla Eklendi", shiftType);
            }
            return new DataResult<ShiftType>(ResultStatus.Error, "Eklemeye çalıştığınız " + shiftType.Name + " halihazırda " + checkSt.Name + " Adıyla veritabanında mevcut", null);
        }

        public MemoryStream CreateExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");

                // Başlıklar
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "StartHour";
                worksheet.Cell(1, 3).Value = "StartMinute";
                worksheet.Cell(1, 4).Value = "EndHour";
                worksheet.Cell(1, 5).Value = "EndMinute";
                worksheet.Cell(1, 6).Value = "Color";

                for (int row = 2; row <= 30; row++)
                {
                    worksheet.Cell(row, 2).Style.NumberFormat.Format = "00";
                    worksheet.Cell(row, 3).Style.NumberFormat.Format = "00";
                    worksheet.Cell(row, 4).Style.NumberFormat.Format = "00";
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "00";
                    //var colorCell = worksheet.Cell(row, 6);
                    //colorCell.Style.Fill.BackgroundColor = XLColor.Black;
                }

                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return stream;
            }
        }




        public async Task<IResult> Delete(ShiftType shiftType)
        {
            var _shiftType = _unitOfWork.ShiftTypes.GetAsync(st => st.Id == shiftType.Id).Result;


            if (_shiftType != null)
            {
                _shiftType.IsDeleted = true;
                _shiftType.ModifiedByName = shiftType.ModifiedByName;
                _shiftType.ModifiedDate = DateTime.Now;
                await _unitOfWork.ShiftTypes.UpdateAsync(_shiftType);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Silindi");
            }
            return new Result(ResultStatus.Error, "Seçili Vardiya Tipi bulunamadı");
        }

        public async Task<IDataResult<IList<ShiftType>>> GetAll()
        {
            var shiftTypes = _unitOfWork.ShiftTypes.GetAllAsync(st => st.IsDeleted == false).Result;
            if (shiftTypes.Count > -1)
            {
                return new DataResult<IList<ShiftType>>(ResultStatus.Success, shiftTypes);
            }
            return new DataResult<IList<ShiftType>>(ResultStatus.Error, "Hiç vardiya tipi bulunamadı", null);
        }

        public async Task<IDataResult<ShiftType>> GetByName(ShiftType shiftType)
        {
            var _shiftType = await _unitOfWork.ShiftTypes.GetAsync(st => st.Name == shiftType.Name);
            if (_shiftType != null)
            {
                return new DataResult<ShiftType>(ResultStatus.Success, _shiftType);
            }
            return new DataResult<ShiftType>(ResultStatus.Error, "Vardiya Tipi bulunamadı", null);
        }

        public async Task<IResult> Update(ShiftType shiftType)
        {
            var _shiftType = _unitOfWork.ShiftTypes.GetAsync(st => st.Id == shiftType.Id).Result;


            if (_shiftType != null)
            {
                _shiftType.Name = shiftType.Name;
                _shiftType.StartTime = shiftType.StartTime;
                _shiftType.EndTime = shiftType.EndTime;
                _shiftType.ModifiedByName = shiftType.ModifiedByName;
                _shiftType.Color = shiftType.Color;
                _shiftType.ModifiedDate = DateTime.Now;

                await _unitOfWork.ShiftTypes.UpdateAsync(_shiftType);
                await _unitOfWork.SaveChangesAsync();

                return new Result(ResultStatus.Success, "Başarıyla Güncellendi");
            }
            return new Result(ResultStatus.Error, "Seçili Vardiya Tipi bulunamadı");
        }

        public async Task<IDataResult<ShiftType>> GetById(int id)
        {
            var shiftType = _unitOfWork.ShiftTypes.Get(id);

            if (shiftType != null)
            {
                return new DataResult<ShiftType>(ResultStatus.Success, shiftType);//dto to entity yüzünden hata çıktı
            }
            return new DataResult<ShiftType>(ResultStatus.Error, "Departman bulunamadı", null);
        }

        public async Task<IResult> BulkInsert(IList<ShiftType> shiftTypes)
        {

            for (int i = 0; i < shiftTypes.Count;)
            {
                var data = _unitOfWork.ShiftTypes.GetAsync(st => st.Name == shiftTypes[i].Name &&
                                                                 st.StartTime == shiftTypes[i].StartTime &&
                                                                 st.EndTime == shiftTypes[i].EndTime &&
                                                                 st.Color == shiftTypes[i].Color).Result;
                
                if (data != null)
                {
                    if (data.IsDeleted == true)
                    {
                        data.IsDeleted = false;
                        data.ModifiedDate = shiftTypes[i].ModifiedDate;
                        data.ModifiedByName = shiftTypes[i].ModifiedByName;
                        await _unitOfWork.ShiftTypes.UpdateAsync(data);

                    }
                    shiftTypes.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            if (shiftTypes.Count == 0)
            {
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Eklendi");
            }
            if (shiftTypes.Count == 1)
            {
                await _unitOfWork.ShiftTypes.AddAsync(shiftTypes[0]);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Başarıyla Eklendi");
            }
            if (shiftTypes.Count > 1)
            {
                try
                {
                    await _unitOfWork.ShiftTypes.BulkInsert(shiftTypes);
                    await _unitOfWork.SaveChangesAsync();
                    return new Result(ResultStatus.Success, "Başarıyla Eklendi");
                }
                catch (Exception ex)
                {
                    return new Result(ResultStatus.Error, "Eksik veya yanlış veriler nedeniyle bir hata oluştu");
                }
            }
            return new Result(ResultStatus.Error, "Vardiya Tipleri Eklenirken Bir sorun oluştu");
        }
    }
}
