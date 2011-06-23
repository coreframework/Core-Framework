using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Forms.NHibernate.Mappings
{
    /// <summary>
    /// FormWidgetAnswer entity mapping.
    /// </summary>
    public class FormWidgetAnswerMapping : ClassMap<FormWidgetAnswer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormWidgetAnswerMapping"/> class.
        /// </summary>
        public FormWidgetAnswerMapping()
        {
            Cache.Region("Forms_FormWidgetAnswers").ReadWrite();
            Table("Forms_FormWidgetAnswers");
            Id(formWidgetAnswer => formWidgetAnswer.Id);
            Map(formWidgetAnswer => formWidgetAnswer.CreateDate);
            Map(formWidgetAnswer => formWidgetAnswer.UserId);

            HasMany(form => form.AnswerValues).KeyColumn("FormAnswerId")
              .Table("Forms_FormAnswerValues")
              .LazyLoad()
              .Cascade.AllDeleteOrphan();
        }
    }
}
