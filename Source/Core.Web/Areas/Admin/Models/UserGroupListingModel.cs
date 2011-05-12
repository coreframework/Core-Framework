using System;
using System.Collections.Generic;
using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Admin.Models
{
    public class UserGroupListingModel : IMappedModel<IEnumerable<UserGroup>, UserGroupListingModel>
    {
        private IList<UserGroupViewModel> userGroups = new List<UserGroupViewModel>();

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public IList<UserGroupViewModel> UserGroups
        {
            get
            {
                return userGroups;
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
        public UserGroupListingModel MapFrom(IEnumerable<UserGroup> from)
        {
            userGroups = new List<UserGroupViewModel>(from.Count());
            foreach (var role in from)
            {
                userGroups.Add(new UserGroupViewModel().MapFrom(role));
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
        public IEnumerable<UserGroup> MapTo(IEnumerable<UserGroup> to)
        {
            throw new NotImplementedException("List model can not be saved.");
        }

        #endregion
    }
}
