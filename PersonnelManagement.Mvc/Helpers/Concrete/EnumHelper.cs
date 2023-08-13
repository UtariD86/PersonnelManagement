using PersonnelManagement.Mvc.Helpers.Abstract;

namespace PersonnelManagement.Mvc.Helpers.Concrete
{
    public class EnumHelper : IEnumHelper
    {
        List<T> IEnumHelper.GetList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        List<KeyValuePair<int, string>> IEnumHelper.GetValuesAndNames<T>()
        {
            return Enum.GetValues(typeof(T))
                   .Cast<Enum>()
                   .Select(e => new KeyValuePair<int, string>(Convert.ToInt32(e), e.ToString()))
                   .ToList();
        }
    }
}
