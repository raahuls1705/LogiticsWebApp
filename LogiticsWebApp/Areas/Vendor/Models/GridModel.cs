using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LogiticsWebApp.Areas.Vendor.Models
{
    public class GridModel<T> where T : class
    {
        public GridModel()
        {
            this.List = new List<T>();
            this.AvailableRows = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "10", Value = "10", Selected = true },
                new SelectListItem(){ Text = "20", Value = "20"},
                new SelectListItem(){ Text = "50", Value = "50"},
                new SelectListItem(){ Text = "100", Value = "100"},
            };
        }
        public List<T> List { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<SelectListItem> AvailableRows { get; set; }
        public string SelectedRow { get; set; }
    }
}
