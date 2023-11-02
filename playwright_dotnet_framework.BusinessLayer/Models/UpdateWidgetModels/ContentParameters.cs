using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels
{
    public class ContentParameters
    {
        public List<string>? ContentFields { get; set; }
        public int ItemsCount { get; set; }
        public WidgetOptions? WidgetOptions { get; set; }
    }
}
