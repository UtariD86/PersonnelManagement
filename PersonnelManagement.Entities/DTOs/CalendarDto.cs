using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Entities.DTOs
{
    public class CalendarDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; } //"yyyy-mm-dd" + "T" + "HH:mm:ss"
        public string end { get; set; } //"yyyy-mm-dd" + "T" + "HH:mm:ss"
        public string color { get; set; }
    }
}
