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
        public bool ShowTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [title linkable].
        /// </summary>
        /// <value><c>true</c> if [title linkable]; otherwise, <c>false</c>.</value>
        public bool TitleLinkable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show summary text].
        /// </summary>
        /// <value><c>true</c> if [show summary text]; otherwise, <c>false</c>.</value>
        public bool ShowSummaryText { get; set; }

        /// <summary>
        /// Gets or sets the content of the show.
        /// </summary>
        /// <value>The content of the show.</value>
        public bool ShowContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show section].
        /// </summary>
        /// <value><c>true</c> if [show section]; otherwise, <c>false</c>.</value>
        public bool ShowSection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show category].
        /// </summary>
        /// <value><c>true</c> if [show category]; otherwise, <c>false</c>.</value>
        public bool ShowCategory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show author].
        /// </summary>
        /// <value><c>true</c> if [show author]; otherwise, <c>false</c>.</value>
        public bool ShowAuthor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show created date].
        /// </summary>
        /// <value><c>true</c> if [show created date]; otherwise, <c>false</c>.</value>
        public bool ShowCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show modified date].
        /// </summary>
        /// <value><c>true</c> if [show modified date]; otherwise, <c>false</c>.</value>
        public bool ShowModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show downloaded link].
        /// </summary>
        /// <value><c>true</c> if [show downloaded link]; otherwise, <c>false</c>.</value>
        public bool ShowDownloadLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is details item.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is details item; otherwise, <c>false</c>.
        /// </value>
        public bool IsDetailsItem { get; set; }

        #endregion

        #region Constructor

        public WidgetDetailsModel(Article article, bool isDetailsItem)
        {
            Article = article;
            IsDetailsItem = isDetailsItem;
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

        private bool GetVisibility(SectionSettingsVisibility visibility)
        {
            if (IsDetailsItem)
            {
                 return visibility == SectionSettingsVisibility.Details || visibility == SectionSettingsVisibility.Both;
            }

            return visibility == SectionSettingsVisibility.Listing || visibility == SectionSettingsVisibility.Both;
        }

        #endregion
    }
}