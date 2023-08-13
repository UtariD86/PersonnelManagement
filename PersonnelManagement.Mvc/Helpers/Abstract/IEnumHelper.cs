using PersonnelManagement.Mvc.Helpers.Concrete;

namespace PersonnelManagement.Mvc.Helpers.Abstract
{
    public interface IEnumHelper
    {
        List<KeyValuePair<int, string>> GetValuesAndNames<T>() where T : Enum;
        List<T> GetList<T>() where T : Enum;
    }
}
