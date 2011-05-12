using System;
using System.ComponentModel.DataAnnotations;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Admin.Models
{
    public class UserGroupViewModel : IMappedModel<UserGroup, UserGroupViewModel>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The role identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The user group name.</value>
        [Required, StringLength(255)]
        public String Name { get; set; }

        [Required, StringLength(4096)]
        public String Description { get; set; }

        #endregion

        #region IMappedModel members

        /// <summary>
        /// Maps current instance from source model.
        /// </summary>
        /// <param name="from">Source model.</param>
        /// <returns>
        /// Mapped target model.
        /// </returns>
        public UserGroupViewModel MapFrom(UserGroup from)
        {
            Id = from.Id;
            Name = from.Name;
            Description = from.Description;

            return this;
        }

        /// <summary>
        /// Maps current instance to source model.
        /// </summary>
        /// <param name="to">Source model.</param>
        /// <returns>
        /// Mapped source model.
        /// </returns>
        public UserGroup MapTo(UserGroup to)
        {
            to.Name = Name;
            to.Description = Description;

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
            if (!String.IsNullOrEmpty(Name))
            {
                return Name;
            }

            return base.ToString();
        }

        #endregion
    }
}