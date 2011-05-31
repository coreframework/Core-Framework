using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPageService : IDataService<Page>
    {
        /// <summary>
        /// Finds the by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        Page FindByUrl(String url);

        /// <summary>
        /// Finds the sibling pages.
        /// </summary>
        /// <param name="parentPageId">The parent page id.</param>
        /// <returns></returns>
        IEnumerable<Page> FindSiblingPages(long? parentPageId);

        /// <summary>
        /// Gets the last order number.
        /// </summary>
        /// <param name="parentPageId">The parent page id.</param>
        /// <returns></returns>
        Int32 GetLastOrderNumber(long? parentPageId);

        /// <summary>
        /// Gets the allowed pages.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <returns></returns>
        IEnumerable<Page> GetAllowedPagesByOperation(ICorePrincipal user, Int32 operationCode);

        /// <summary>
        /// Gets the first allowed page.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <returns></returns>
        Page GetFirstAllowedPage(ICorePrincipal user, Int32 operationCode);
    }
}
