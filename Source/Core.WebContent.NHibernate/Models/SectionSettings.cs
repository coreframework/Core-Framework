using System;
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
        public virtual bool ShowTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [title linkable].
        /// </summary>
        /// <value><c>true</c> if [title linkable]; otherwise, <c>false</c>.</value>
        public virtual bool TitleLinkable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show summary text].
        /// </summary>
        /// <value><c>true</c> if [show summary text]; otherwise, <c>false</c>.</value>
        public virtual bool ShowSummaryText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show section].
        /// </summary>
        /// <value><c>true</c> if [show section]; otherwise, <c>false</c>.</value>
        public virtual bool ShowSection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show category].
        /// </summary>
        /// <value><c>true</c> if [show category]; otherwise, <c>false</c>.</value>
        public virtual bool ShowCategory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show author].
        /// </summary>
        /// <value><c>true</c> if [show author]; otherwise, <c>false</c>.</value>
        public virtual bool ShowAuthor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show created date].
        /// </summary>
        /// <value><c>true</c> if [show created date]; otherwise, <c>false</c>.</value>
        public virtual bool ShowCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show modified date].
        /// </summary>
        /// <value><c>true</c> if [show modified date]; otherwise, <c>false</c>.</value>
        public virtual bool ShowModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show PDF icon].
        /// </summary>
        /// <value><c>true</c> if [show PDF icon]; otherwise, <c>false</c>.</value>
        public virtual bool ShowPdfIcon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show print icon].
        /// </summary>
        /// <value><c>true</c> if [show print icon]; otherwise, <c>false</c>.</value>
        public virtual bool ShowPrintIcon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show email icon].
        /// </summary>
        /// <value><c>true</c> if [show email icon]; otherwise, <c>false</c>.</value>
        public virtual bool ShowEmailIcon { get; set; }

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
