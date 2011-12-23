using System;
using System.ComponentModel.DataAnnotations;

namespace Core.FormLogin.Models
{
    public class LoginWidgetViewModel
    {
        #region Properties

        public long PageWidgetId { get; set; }

        public bool ShowTitle { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required, StringLength(255)]
        public String UsernameOrEmail { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required, StringLength(255)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user session should be saved across browser session.
        /// </summary>
        /// <value><c>true</c> if user session should be saved across browser session; otherwise, <c>false</c>.</value>
        public bool RememberMe { get; set; }

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