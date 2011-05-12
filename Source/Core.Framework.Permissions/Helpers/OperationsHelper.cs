using System;
using System.Collections.Generic;
using System.Linq;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Permissions.Helpers
{
    /// <summary>
    /// Provides helper methods for work with permission operations.
    /// </summary>
    public static class OperationsHelper
    {
        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns></returns>
        public static IEnumerable<IPermissionOperation> GetOperations<TEnum>()
        where TEnum : struct
        {
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var description  = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(OperationDescriptionAttribute), false).FirstOrDefault() as OperationDescriptionAttribute; 

                if (description != null)
                {
                    yield return new PermissionOperation { Key = (int) value,
                                                           Title = Enum.GetName(typeof(TEnum), value),
                                                           Area = description.Area,
                                                           OperationLevel = description.OperationLevel,
                                                           GuestDefaultAcess = description.GuestDefaultAcess,
                                                           OwnerDefaultAcess = description.OwnerDefaultAcess,
                                                           UserDefaultAccess = description.UserDefaultAccess
                    };
                }
            }
        }
    }
}
