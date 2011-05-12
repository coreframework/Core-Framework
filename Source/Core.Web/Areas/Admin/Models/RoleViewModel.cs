using System;
using System.ComponentModel.DataAnnotations;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Admin.Models
{
    /// <summary>
    /// Role view model.
    /// </summary>
    public class RoleViewModel : IMappedModel<Role, RoleViewModel>
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
        /// <value>The role name.</value>
        [Required, StringLength(255)]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable { get; set; }

        #endregion

        #region IMappedModel members

        /// <summary>
        /// Maps current instance from source model.
        /// </summary>
        /// <param name="from">Source model.</param>
        /// <returns>
        /// Mapped target model.
        /// </returns>
        public RoleViewModel MapFrom(Role from)
        {
            Id = from.Id;
            Name = from.Name;
            IsEditable = !from.IsSystemRole;
            return this;
        }

        /// <summary>
        /// Maps current instance to source model.
        /// </summary>
        /// <param name="to">Source model.</param>
        /// <returns>
        /// Mapped source model.
        /// </returns>
        public Role MapTo(Role to)
        {
            to.Name = Name;
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