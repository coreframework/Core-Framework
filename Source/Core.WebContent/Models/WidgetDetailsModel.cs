using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Static;

namespace Core.WebContent.Models
{
    public class WidgetDetailsModel
    {
        #region Properties

        public Article Article { get; set; }

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
        /// Gets or sets the content of the show.
        /// </summary>
        /// <value>The content of the show.</value>
        public virtual bool ShowContent { get; set; }

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
        /// Gets or sets a value indicating whether [show downloaded link].
        /// </summary>
        /// <value><c>true</c> if [show downloaded link]; otherwise, <c>false</c>.</value>
        public virtual bool ShowDownloadLink { get; set; }

        #endregion

        #region Constructor

        public WidgetDetailsModel(Article article)
        {
            Article = article;
            BindSettings();
        }

        #endregion

        #region Helper Methods

        private void BindSettings()
        {
            var settings = Article.Category.Section.SectionSettings;
            ShowTitle = GetVisibility(settings.ShowTitle);
            TitleLinkable = GetVisibility(settings.TitleLinkable);
            ShowSummaryText = GetVisibility(settings.ShowSummaryText);
            ShowContent = GetVisibility(settings.ShowContent);
            ShowSection = GetVisibility(settings.ShowSection);
            ShowCategory = GetVisibility(settings.ShowCategory);
            ShowAuthor = GetVisibility(settings.ShowAuthor);
            ShowCreatedDate = GetVisibility(settings.ShowCreatedDate);
            ShowModifiedDate = GetVisibility(settings.ShowModifiedDate);
            ShowDownloadLink = GetVisibility(settings.ShowDownloadLink);
        }

        private static bool GetVisibility(SectionSettingsVisibility visibility)
        {
            return visibility == SectionSettingsVisibility.Details || visibility == SectionSettingsVisibility.Both;
        }

        #endregion


    }
}