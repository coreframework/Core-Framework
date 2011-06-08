using System;

namespace Core.Framework.Permissions.Models
{
    public interface IRole
    {
        long Id { get; set; }

        String Name { get; set; }
    }
}
