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

        public virtual long Id { get; set; }

        public bool AllowManage { get; set; }

        public String SubmitButtonText { get; set; }

        public String ResetButtonText { get; set; }

        public bool ShowSubmitButton { get; set; }

        public bool ShowResetButton { get; set; }

        public String SelectedCulture { get; set; }

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