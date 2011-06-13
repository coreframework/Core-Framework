using System;
using System.Collections.Generic;
using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;
using Core.Web.Helpers.HtmlExtensions;

namespace Core.Web.Areas.Admin.Models
{
    public class UserListingModel : IMappedModel<IEnumerable<User>, UserListingModel>
    {
        private IList<UserViewModel> users = new List<UserViewModel>();
        private readonly int? pageIndex;

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public PagedList<UserViewModel> Users { get; set; }

        public UserListingModel(int? pageIndex)
        {
            this.pageIndex = pageIndex;
        }

        #region IMappedModel members

        /// <summary>
        /// Maps current instance from source model.
        /// </summary>
        /// <param name="from">Source model.</param>
        /// <returns>
        /// Mapped target model.
        /// </returns>
        public UserListingModel MapFrom(IEnumerable<User> from)
        {
            users = new List<UserViewModel>(from.Count());
            foreach (var role in from)
            {
                users.Add(new UserViewModel().MapFrom(role));
            }
            Users = users.ToPagedList(pageIndex ?? 0, 1);
            return this;
        }

        /// <summary>
        /// Maps current instance to source model.
        /// </summary>
        /// <param name="to">Source model.</param>
        /// <returns>
        /// Mapped source model.
        /// </returns>
        public IEnumerable<User> MapTo(IEnumerable<User> to)
        {
            throw new NotImplementedException("List model can not be saved.");
        }

        #endregion
    }
}