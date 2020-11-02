using LogiticsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LogiticsWebApp.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() =>
            {
                if (TempData.ContainsKey("Notifications"))
                {
                    var notifyData = JsonConvert.DeserializeObject<NotifyDataModel>(TempData["Notifications"].ToString());

                    return View("~/Views/Components/Notification/Default.cshtml", notifyData);
                }
                return View();
            });
        }
    }
}
