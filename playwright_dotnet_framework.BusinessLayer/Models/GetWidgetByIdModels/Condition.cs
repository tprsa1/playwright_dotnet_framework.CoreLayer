using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels
{
    public class Condition
    {
        public string? FilteringField { get; set; }
        public string? ConditionValue { get; set; }
        public string? Value { get; set; }
    }
}
