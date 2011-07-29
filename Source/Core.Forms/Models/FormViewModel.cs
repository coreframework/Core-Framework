using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Forms.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;

namespace Core.Forms.Models
{
    public class FormViewModel : IMappedModel<Form, FormViewModel>
    {
        private IDictionary<String, String> _cultures;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual long Id { get; set; }

        public bool AllowManage { get; set; }

        /// <summary>
        /// Gets or sets the submit button text.
        /// </summary>
        /// <value>The submit button text.</value>
        public String SubmitButtonText { get; set; }

        /// <summary>
        /// Gets or sets the reset button text.
        /// </summary>
        /// <value>The reset button text.</value>
        public String ResetButtonText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show submit button].
        /// </summary>
        /// <value><c>true</c> if [show submit button]; otherwise, <c>false</c>.</value>
        public bool ShowSubmitButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show reset button].
        /// </summary>
        /// <value><c>true</c> if [show reset button]; otherwise, <c>false</c>.</value>
        public bool ShowResetButton { get; set; }

        /// <summary>
        /// Gets or sets the selected culture.
        /// </summary>
        /// <value>The selected culture.</value>
        public String SelectedCulture { get; set; }

        /// <summary>
        /// Gets or sets the cultures.
        /// </summary>
        /// <value>The cultures.</value>
        public IDictionary<String, String> Cultures
        {
            get { return _cultures ?? (_cultures = CultureHelper.GetAvailableCultures()); }
            set { _cultures = value; }
        }

        public FormViewModel MapFrom(Form from)
        {
            Id = from.Id;
            ShowSubmitButton = from.ShowSubmitButton;
            ShowResetButton = from.ShowResetButton;
            MapLocaleFrom(from.CurrentLocale as FormLocale);
            return this;
        }

        public Form MapTo(Form to)
        {
            to.Id = Id;
            to.ShowSubmitButton = ShowSubmitButton;
            to.ShowResetButton = ShowResetButton;
            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((FormLocale)to.CurrentLocale);

            return to;
        }

        public FormViewModel MapLocaleFrom(FormLocale locale)
        {
            Title = locale.Title;
            SubmitButtonText = locale.SubmitButtonText;
            ResetButtonText = locale.ResetButtonText;
            SelectedCulture = locale.Culture;

            return this;
        }

        public FormLocale MapLocaleTo(FormLocale locale)
        {
            locale.Title = Title;
            locale.SubmitButtonText = SubmitButtonText;
            locale.ResetButtonText = ResetButtonText;
            if (SelectedCulture!=null)
                locale.Culture = SelectedCulture;
            return locale;
        }
    }
}