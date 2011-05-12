// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkController.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Core.Logging;
using Framework.Core.Infrastructure;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;

namespace Framework.MVC.Controllers
{
    /// <summary>
    /// Provides controller basic functionality.
    /// </summary>
    public class FrameworkController : Controller
    {
        #region Fields

        /// <summary>
        /// Key for flash messages queue.
        /// </summary>
        public const String MessagesKey = "FlashMessages";

        private ILogger logger = NullLogger.Instance;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get
            {
                return logger;
            }
            set
            {
                logger = value;
            }
        }

        #endregion

        #region Messages

        /// <summary>
        /// Adds the message to message queue.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="parameters">The parameters.</param>
        protected void AddMessage(String message, MessageType messageType, params Object[] parameters)
        {
            var messages = TempData[MessagesKey] as Queue<Message> ?? new Queue<Message>();
            messages.Enqueue(new Message { Text = String.Format(message, parameters), MessageType = messageType });
            TempData[MessagesKey] = messages;
        }

        /// <summary>
        /// Adds notice message to message queue.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        protected void Success(String message, params Object[] parameters)
        {
            AddMessage(message, MessageType.Success);
        }

        /// <summary>
        /// Adds warning message to message queue.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        protected void Notice(String message, params Object[] parameters)
        {
            AddMessage(message, MessageType.Notice);
        }

        /// <summary>
        /// Adds error message to message queue.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        protected void Error(String message, params Object[] parameters)
        {
            AddMessage(message, MessageType.Error);
        }

        #endregion

        #region Localization

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        protected String Translate(String key, String scope)
        {
            return HttpContext.Translate(key, scope);
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        protected String Translate(String key, IEnumerable<String> scope)
        {
            return HttpContext.Translate(key, scope);
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> specified.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>String localized for current thread culture.</returns>
        protected String Translate(String key)
        {
            var scope = String.Empty;
            if (key.StartsWith(ResourceHelper.ScopeSeparator))
            {
                key = key.Remove(0, ResourceHelper.ScopeSeparator.Length);
                scope = ResourceHelper.GetControllerScope(this);
            }

            return Translate(key, scope);
        }

        #endregion
    }
}