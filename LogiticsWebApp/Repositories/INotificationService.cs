using LogiticsWebApp.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Repositories
{
    public interface INotificationService
    {
        void Notification(ENotifyType type, string message, bool encode = true);
        void SuccessNotification(string message, bool encode = true);
        void WarningNotification(string message, bool encode = true);
        void InfoNotification(string message, bool encode = true);
        void ErrorNotification(string message, bool encode = true);
        void ErrorNotification(Exception ex, bool logException = true);

    }
}
