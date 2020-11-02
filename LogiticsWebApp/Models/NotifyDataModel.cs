using LogiticsWebApp.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Models
{
    public class NotifyDataModel
    {
        public ENotifyType Status { get; set; }
        public string Message { get; set; }
        public bool HtmlEncoded { get; set; }
    }
}
