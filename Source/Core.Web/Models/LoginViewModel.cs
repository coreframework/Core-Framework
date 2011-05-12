using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Web.Models
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginViewModel
    {
        #region Properties

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
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        public String ReturnUrl { get; set; }

        #endregion
    }
}