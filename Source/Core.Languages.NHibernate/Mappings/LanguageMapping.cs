using Core.Languages.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Languages.NHibernate.Mappings
{
    public class LanguageMapping : ClassMap<Language>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageMapping"/> class.
        /// </summary>
        public LanguageMapping()
        {
            Cache.Region("Languages_Languages").ReadWrite();
            Table("Languages_Languages");
            Id(language => language.Id);
            Map(language => language.Title).Length(255);
            Map(language => language.Code).Length(3);
            Map(language => language.Culture).Length(10);
        }
    }
}
