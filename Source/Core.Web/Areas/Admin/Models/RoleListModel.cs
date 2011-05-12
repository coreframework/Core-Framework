using System;
using System.Collections.Generic;
using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Admin.Models
{
    /// <summary>
    /// Role listing model.
    /// </summary>
    public class RoleListModel : IMappedModel<IEnumerable<Role>, RoleListModel>
    {
        private IList<RoleViewModel> roles = new List<RoleViewModel>();

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public IList<RoleViewModel> Roles
        {
            get
            {
                return roles;
            }
        }

        #region IMappedModel members

        /// <summary>
        /// Maps current instance from source model.
        /// </summary>
        /// <param name="from">Source model.</param>
        /// <returns>
        /// Mapped target model.
        /// </returns>
        public RoleListModel MapFrom(IEnumerable<Role> from)
        {
            roles = new List<RoleViewModel>(from.Count());
            foreach (var role in from)
            {
                roles.Add(new RoleViewModel().MapFrom(role));
            }
            return this;
        }

        /// <summary>
        /// Maps current instance to source model.
        /// </summary>
        /// <param name="to">Source model.</param>
        /// <returns>
        /// Mapped source model.
        /// </returns>
        public IEnumerable<Role> MapTo(IEnumerable<Role> to)
        {
            throw new NotImplementedException("List model can not be saved.");
        }

        #endregion
    }
}