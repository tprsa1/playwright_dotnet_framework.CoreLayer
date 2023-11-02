using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels
{
    public class Order
    {
        public string? SortingColumn { get; set; }
        public bool IsAsc { get; set; }
    }
}
