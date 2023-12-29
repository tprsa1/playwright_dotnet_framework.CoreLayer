using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels
{
    public class Result
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Name { get; set; }
        public long StartTime { get; set; }
        public Dictionary<string, string>? Values { get; set; }
    }
}
