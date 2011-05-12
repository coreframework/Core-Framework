// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.Core.Infrastructure
{
    /// <summary>
    /// Contains flash message data.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <value>The message text.</value>
        public String Text { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public MessageType MessageType { get; set; }
    }
}