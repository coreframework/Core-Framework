using System;

namespace Core.Framework.Permissions.Models
{
    public class PermissionOperation: IPermissionOperation
    {
        public int Key { get; set; }

        public String Title { get; set; }

        public PermissionArea Area { get; set; }

        public PermissionOperationLevel OperationLevel { get; set; }

        public bool UserDefaultAccess { get; set; }

        public bool OwnerDefaultAcess { get; set; }

        public bool GuestDefaultAcess { get; set; }
    }
}
