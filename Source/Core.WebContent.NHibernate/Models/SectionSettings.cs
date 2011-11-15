using System;
using Core.WebContent.NHibernate.Static;
using FluentNHibernate.Data;

namespace Core.WebContent.NHibernate.Models
{
    public class SectionSettings: Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [show title].
        /// </summary>
        /// <value><c>true</c> if [show title]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [title linkable].
        /// </summary>
        /// <value><c>true</c> if [title linkable]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility TitleLinkable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show summary text].
        /// </summary>
        /// <value><c>true</c> if [show summary text]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowSummaryText { get; set; }

        /// <summary>
        /// Gets or sets the content of the show.
        /// </summary>
        /// <value>The content of the show.</value>
        public virtual SectionSettingsVisibility ShowContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show section].
        /// </summary>
        /// <value><c>true</c> if [show section]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowSection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show category].
        /// </summary>
        /// <value><c>true</c> if [show category]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowCategory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show author].
        /// </summary>
        /// <value><c>true</c> if [show author]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowAuthor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show created date].
        /// </summary>
        /// <value><c>true</c> if [show created date]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show modified date].
        /// </summary>
        /// <value><c>true</c> if [show modified date]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show downloaded link].
        /// </summary>
        /// <value><c>true</c> if [show downloaded link]; otherwise, <c>false</c>.</value>
        public virtual SectionSettingsVisibility ShowDownloadLink { get; set; }

        /// <summary>
        /// Gets or sets the alternative read more text.
        /// </summary>
        /// <value>The alternative read more text.</value>
        public virtual String AlternativeReadMoreText { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public virtual Section Section { get; set; }

        #endregion
    }
}
