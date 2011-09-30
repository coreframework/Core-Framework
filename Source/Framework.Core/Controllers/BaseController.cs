// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Core.Logging;
using Framework.Core.Infrastructure;


namespace Framework.Core.Controllers
{
    /// <summary>
    /// Provides controller basic functionality.
    /// </summary>
    public class BaseController : Controller
    {
        #region Fields

        /// <summary>
        /// Key for flash messages queue.
        /// </summary>
        public static readonly String MessagesKey = "FlashMessages";

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
        protected void Success(String message)
        {
            AddMessage(message, MessageType.Success);
        }

        /// <summary>
        /// Adds warning message to message queue.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void Notice(String message)
        {
            AddMessage(message, MessageType.Notice);
        }

        /// <summary>
        /// Adds information message to message queue.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void Information(String message)
        {
            AddMessage(message, MessageType.Info);
        }

        /// <summary>
        /// Adds error message to message queue.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void Error(String message)
        {
            AddMessage(message, MessageType.Error);
        }

        #endregion
    }
}