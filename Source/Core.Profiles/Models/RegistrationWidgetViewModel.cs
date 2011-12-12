using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.Framework.Permissions.Models;
using Framework.Core.DomainModel;
using Framework.Mvc.Metadata.Attributes;

namespace Core.Profiles.Models
{
    public class RegistrationWidgetViewModel : IMappedModel<BaseUser, RegistrationWidgetViewModel>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the page widget id.
        /// </summary>
        /// <value>The page widget id.</value>
        public long PageWidgetId { get; set; }

        /// <summary>
        /// Gets or sets the profile type id.
        /// </summary>
        /// <value>The profile type id.</value>
        public long ProfileTypeId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required(ErrorMessage = @"Error"), StringLength(255), Email]
        [AllowHtml]
        public String Email { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required, StringLength(255)]
        public String Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required, StringLength(255)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required, StringLength(255)]
        [DataType(DataType.Password)]
        public String PasswordConfirmation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful registration.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is successful registration; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessfulRegistration { get; set; }

        public NHibernate.Models.RegistrationWidget Widget { get; set; } 

        #endregion

        public RegistrationWidgetViewModel MapFrom(BaseUser from)
        {
            Email = from.Email;
            Username = from.Username;

            return this;
        }

        /// <summary>
        /// Maps current instance to source model.
        /// </summary>
        /// <param name="to">Source model.</param>
        /// <returns>
        /// Mapped source model.
        /// </returns>
        public BaseUser MapTo(BaseUser to)
        {
            to.Email = Email;
            to.Username = Username;

            return to;
        }
    }
}