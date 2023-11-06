using System;

namespace playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels
{
    public class AppliedFilter
    {
        public string? Owner { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Condition> Conditions { get; set; }
        public List<Order> Orders { get; set; }
        public string? Type { get; set; }
    }
}
