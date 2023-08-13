using ClosedXML.Excel;
using Newtonsoft.Json;
using PersonnelManagement.Mvc.Helpers.Abstract;
using System.Drawing;
using System.Reflection;

namespace PersonnelManagement.Mvc.Helpers.Concrete
{
    public class ThemeColorHelper:IThemeColorHelper
    {
        //private Dictionary<string, Color> themeColors;
        //private Dictionary<string, string> closedXmlColors;

        //public ThemeColorHelper()
        //{
        //    closedXmlColors = GetClosedXmlStaticColors();

        //    // Dosya yolu
        //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "js", "datas", "xmlColors.json");

        //    // Dosyayı okuma
        //    var json = File.ReadAllText(filePath);

        //    // JSON'u dönüştürme
        //    var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<int>>>>(json);

        //    // ThemeColors'u System.Drawing.Color olarak dönüştürme
        //    themeColors = new Dictionary<string, Color>();
        //    foreach (var item in data["Theme Colors"])
        //    {
        //        themeColors[item.Key] = Color.FromArgb(item.Value[0], item.Value[1], item.Value[2]);
        //    }
        //}

        //private Dictionary<string, string> GetClosedXmlStaticColors()
        //{
        //    var fields = typeof(XLColor).GetFields(BindingFlags.Static | BindingFlags.Public);
        //    Console.WriteLine("Statik alan sayısı: " + fields.Length);
        //    foreach (var field in fields)
        //    {
        //        Console.WriteLine(field.Name);
        //    }
        //    var mappings = new Dictionary<string, string>();

        //    foreach (var field in typeof(ClosedXML.Excel.XLColor).GetFields(BindingFlags.Static | BindingFlags.Public))
        //    {
        //        if (field.FieldType == typeof(ClosedXML.Excel.XLColor))
        //        {
        //            var colorValue = field.GetValue(null) as ClosedXML.Excel.XLColor;
        //            if (colorValue != null && colorValue.ColorType == ClosedXML.Excel.XLColorType.Color)
        //            {
        //                string colorString = colorValue.ToString();  // Örneğin: {FF123456}
        //                mappings.Add(field.Name, colorString);
        //            }
        //        }
        //    }

        //    return mappings;
        //}

        //public Color ConvertThemeColorToRgb(string themeColorName)
        //{
        //    if (closedXmlColors.TryGetValue(themeColorName, out var hexColor))
        //    {
        //        if (themeColors.TryGetValue(hexColor, out var rgbColor))
        //        {
        //            return rgbColor;
        //        }
        //    }
        //    return Color.Empty;
        //}
    }
}
