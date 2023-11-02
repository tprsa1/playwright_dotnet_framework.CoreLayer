using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels
{
    public class Condition
    {
        public string? FilteringField { get; set; }
        public string? ConditionType { get; set; } // 'condition' is a reserved keyword
        public string? Value { get; set; }
    }
}
