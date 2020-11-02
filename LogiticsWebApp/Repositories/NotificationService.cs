using LogiticsWebApp.Models;
using LogiticsWebApp.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Repositories
{

    public class NotificationService : INotificationService
    {
        #region fields
        private readonly HttpContext _httpContext;
        private readonly ILogger _logger;
        readonly ITempDataDictionary TempData;
        #endregion

        #region ctor
        public NotificationService(IHttpContextAccessor httpContextAccessor,
        //ILogger logger,
        ITempDataDictionaryFactory tempData)
        {
            _httpContext = httpContextAccessor.HttpContext;
            //_logger = logger;
            TempData = tempData.GetTempData(_httpContext);
        }
        #endregion

        #region Utility
        /// <summary>
        /// Save message into TempData
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="encode">A value indicating whether the message should not be encoded</param>
        protected virtual void PrepareTempData(ENotifyType type, string message, bool encode = true)
        {
            var notificationMessages = new NotifyDataModel
            {
                Status = type,
                Message = message,
                HtmlEncoded = encode
            };

            TempData["Notifications"] = JsonConvert.SerializeObject(notificationMessages);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Display notification
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="encode">A value indicating whether the message should not be encoded</param>
        public virtual void Notification(ENotifyType type, string message, bool encode = true)
        {
            PrepareTempData(type, message, encode);
        }

        /// <summary>
        /// Display success notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="encode">A value indicating whether the message should not be encoded</param>
        public virtual void SuccessNotification(string message, bool encode = true)
        {
            PrepareTempData(ENotifyType.success, message, encode);
        }

        /// <summary>
        /// Display warning notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="encode">A value indicating whether the message should not be encoded</param>
        public virtual void WarningNotification(string message, bool encode = true)
        {
            PrepareTempData(ENotifyType.warning, message, encode);
        }

        /// <summary>
        /// Display Info notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="encode">A value indicating whether the message should not be encoded</param>
        public virtual void InfoNotification(string message, bool encode = true)
        {
            PrepareTempData(ENotifyType.info, message, encode);
        }

        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="encode">A value indicating whether the message should not be encoded</param>
        public virtual void ErrorNotification(string message, bool encode = true)
        {
            PrepareTempData(ENotifyType.danger, message, encode);
        }

        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="logException">A value indicating whether exception should be logged</param>
        public virtual void ErrorNotification(Exception ex, bool logException = true)
        {
            if (ex == null)
                return;

            //if (logException)
            //_logger.LogError(ex, ex.Message, new { UserNameEmail = _httpContext.User.Identity.Name });

            ErrorNotification(ex.Message);
        }

        #endregion
    }
}
