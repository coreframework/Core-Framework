using System;
using System.ComponentModel.DataAnnotations;

namespace Core.OpenIDLogin.Models
{
    public class OpenIDLoginWidgetViewModel
    {
        #region Properties

        public long PageWidgetId { get; set; }

        public bool ShowTitle { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required, StringLength(255)]
        public String UserOpenId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful login.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is successful login; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessfulLogin { get; set; }

        #endregion
    }
}