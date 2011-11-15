using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Models;
using Framework.Core.Services;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface ICategoryService : IDataService<WebContentCategory>
    {
        /// <summary>
        /// Gets the allowed categories by operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        IEnumerable<WebContentCategory> GetAllowedCategoriesByOperation(ICorePrincipal user, Int32 operation);

        /// <summary>
        /// Gets the allowed section categories by operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="sectionId">The section id.</param>
        /// <returns></returns>
        IEnumerable<WebContentCategory> GetAllowedSectionCategoriesByOperation(ICorePrincipal user, Int32 operation, long sectionId);
    }
}
