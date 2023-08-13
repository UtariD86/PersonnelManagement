using PersonnelManagement.Mvc.Areas.Admin.Models;
using System.Text.Json;

namespace PersonnelManagement.Mvc.Helpers.Abstract
{
    public interface IPhoneCodesHelper
    {
        Task<List<CountryCodeModel>> GetPhoneCodesFromFile(string filePath);
    }
}
