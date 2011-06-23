using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Forms.NHibernate.Mappings
{
    public class FormWidgetAnswerValueMapping : ClassMap<FormWidgetAnswerValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormWidgetAnswerValueMapping"/> class.
        /// </summary>
        public FormWidgetAnswerValueMapping()
        {
            Cache.Region("Forms_FormAnswerValues").ReadWrite();
            Table("Forms_FormAnswerValues");
            Id(widgetAnswerValue => widgetAnswerValue.Id);
            Map(widgetAnswerValue => widgetAnswerValue.Field);
            Map(widgetAnswerValue => widgetAnswerValue.Value);

            References(widgetAnswerValue => widgetAnswerValue.Answer).Column("FormAnswerId");
        }
    }
}
