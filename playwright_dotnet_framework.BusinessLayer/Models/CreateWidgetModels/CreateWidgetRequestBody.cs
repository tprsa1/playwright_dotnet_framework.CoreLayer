using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels
{
    public class CreateWidgetRequestBody
    {
        public string? Description { get; set; }
        public string? Owner { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? WidgetType { get; set; }
        public ContentParameters ContentParameters { get; set; }
        public List<AppliedFilter> AppliedFilters { get; set; }
        public Content Content { get; set; }
    }
}

