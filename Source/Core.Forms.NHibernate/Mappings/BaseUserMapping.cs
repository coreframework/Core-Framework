using Core.Framework.Permissions.Models;
using FluentNHibernate.Mapping;

namespace Core.Forms.NHibernate.Mappings
{
    public class BaseUserMapping : ClassMap<BaseUser>
    {
        public BaseUserMapping()
        {
            Cache.Region("Users").ReadWrite();
            Table("Users");
            Id(user => user.Id);
            Map(user => user.Username).Length(255);
        }
    }
}
