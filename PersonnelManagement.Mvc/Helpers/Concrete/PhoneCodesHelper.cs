using PersonnelManagement.Mvc.Areas.Admin.Models;
using PersonnelManagement.Mvc.Helpers.Abstract;
using System.Text.Json;

namespace PersonnelManagement.Mvc.Helpers.Concrete
{
    public class PhoneCodesHelper : IPhoneCodesHelper
    {
        public async Task<List<CountryCodeModel>> GetPhoneCodesFromFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            { 
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return await JsonSerializer.DeserializeAsync<List<CountryCodeModel>>(fs, options);
            }
        }
    }
}
