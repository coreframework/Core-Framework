using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class SchemaInfoMapping : ClassMap<SchemaInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaInfoMapping"/> class.
        /// </summary>
        public SchemaInfoMapping()
        {
            Cache.Region("SchemaInfo").ReadWrite();
            Table("SchemaInfo");
            Id(migration => migration.Version).Column("Version").GeneratedBy.Assigned();
        }
    }
}
