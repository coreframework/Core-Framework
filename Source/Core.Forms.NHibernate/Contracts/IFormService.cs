using System;
using System.Collections.Generic;
using Core.Forms.NHibernate.Models;
using Core.Framework.Permissions.Models;
using Framework.Core.Services;

namespace Core.Forms.NHibernate.Contracts
{
    public interface IFormService : IDataService<Form>
    {
        /// <summary>
        /// Gets the allowed forms by operation code.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operation">The operation code.</param>
        /// <returns></returns>
        IEnumerable<Form> GetAllowedFormsByOperation(ICorePrincipal user, Int32 operation);
    }
}
