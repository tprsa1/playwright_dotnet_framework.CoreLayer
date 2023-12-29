using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels
{
    public class PageInfo
    {
        public int Number { get; set; }
        public int Size { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
    }
}
