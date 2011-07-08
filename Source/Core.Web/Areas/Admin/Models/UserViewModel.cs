using System;
using System.ComponentModel.DataAnnotations;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.MVC.Metadata.Attributes;

namespace Core.Web.Areas.Admin.Models
{
    public class UserViewModel : IMappedModel<User, UserViewModel>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The user identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required ( ErrorMessage = @"Error"), StringLength(255), Email]
        public String Email { get; set; }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        [Required, StringLength(255)]
        public String Nickname { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [StringLength(255)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [DataType("List")]
        public UserStatus Status { get; set; }

        #region IMappedModel members

        /// <summary>
        /// Maps current instance from source model.
        /// </summary>
        /// <param name="from">Source model.</param>
        /// <returns>
        /// Mapped target model.
        /// </returns>
        public UserViewModel MapFrom(User from)
        {
            Id = from.Id;            
            Email = from.Email;
            Nickname = from.Username;
            Status = from.Status;

            return this;
        }

        /// <summary>
        /// Maps current instance to source model.
        /// </summary>
        /// <param name="to">Source model.</param>
        /// <returns>
        /// Mapped source model.
        /// </returns>
        public User MapTo(User to)
        {
            to.Email = Email;
            to.Username = Nickname;
            to.Status = Status;

            return to;
        }

        #endregion

        #region Object members

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            if (!String.IsNullOrEmpty(Nickname))
            {
                return Nickname;
            }

            return base.ToString();
        }

        #endregion
    }
}