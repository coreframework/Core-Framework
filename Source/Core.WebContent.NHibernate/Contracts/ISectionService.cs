using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Models;
using Framework.Core.Services;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface ISectionService : IDataService<Section>
    {
        /// <summary>
        /// Gets the allowed forms by operation code.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operation">The operation code.</param>
        /// <returns></returns>
        IEnumerable<Section> GetAllowedSectionsByOperation(ICorePrincipal user, Int32 operation);
    }
}
