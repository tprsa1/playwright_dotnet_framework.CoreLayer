using System;

namespace playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels
{
    public class UpdateWidgetRequest
    {
        public ContentParameters? ContentParameters { get; set; }
        public string? Description { get; set; }
        public string? Owner { get; set; }
        public string? Name { get; set; }
        public string? WidgetType { get; set; }
        public List<Filter>? Filters { get; set; }
        public List<string>? FilterIds { get; set; }
    }
}
