using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels
{
    public class AppliedFilter
    {
        public string? Owner { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Condition>? Conditions { get; set; }
        public List<Order>? Orders { get; set; }
        public string? Type { get; set; }
    }
}
