using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Forms.NHibernate.Helpers;
using Core.Forms.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;

namespace Core.Forms.Models
{
    public class FormElementViewModel : IMappedModel<FormElement, FormElementViewModel>
    {
        #region Fields

        private List<ElementTypeDescriptionModel> types;

        private IDictionary<String, String> cultures;

        #endregion

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public virtual String Values { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is required; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>
        public List<ElementTypeDescriptionModel> Types
        {
            get { return types ?? (types = BindElementTypes()); }
        }

        /// <summary>
        /// Gets or sets the validation regex template.
        /// </summary>
        /// <value>The validation regex template.</value>
        public RegexTemplate? RegexTemplate { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public FormElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the length of the max.
        /// </summary>
        /// <value>The length of the max.</value>
        public long? MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the form id.
        /// </summary>
        /// <value>The form id.</value>
        public long? FormId { get; set; }

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
            get { return cultures ?? (cultures = CultureHelper.GetAvailableCultures()); }
            set { cultures = value; }
        }

        public FormElementViewModel MapFrom(FormElement from)
        {
            Id = from.Id;
            RegexTemplate = from.RegexTemplate;
            MaxLength = from.MaxLength;
            Type = from.Type;
            IsRequired = from.IsRequired;
            MapLocaleFrom(from.CurrentLocale as FormElementLocale);

            return this;
        }

        public FormElement MapTo(FormElement to)
        {
            to.Id = Id;
            to.IsRequired = IsRequired;
            to.Type = Type;
            if (RegexTemplate != null) to.RegexTemplate = (RegexTemplate) RegexTemplate;
            to.MaxLength = MaxLength;
            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((FormElementLocale)to.CurrentLocale);
            return to;
        }

        public FormElementViewModel MapLocaleFrom(FormElementLocale locale)
        {
            Title = locale.Title;
            Values = locale.ElementValues;
            SelectedCulture = locale.Culture;

            return this;
        }

        public FormElementLocale MapLocaleTo(FormElementLocale locale)
        {
            locale.Title = Title;
            locale.ElementValues = Values;
            if (SelectedCulture != null)
                locale.Culture = SelectedCulture;
            return locale;
        }

        /// <summary>
        /// Binds the element types.
        /// </summary>
        /// <returns></returns>
        private static List<ElementTypeDescriptionModel> BindElementTypes()
        {
            var result = new List<ElementTypeDescriptionModel>();

            foreach (var value in Enum.GetValues(typeof(FormElementType)))
            {
                var elementType = new ElementTypeDescriptionModel {Type = Enum.GetName(typeof(FormElementType), value) };

                var description = value.GetType().GetField(value.ToString()).GetCustomAttributes(
                       typeof(ElementTypeDescriptionAttribute), false).FirstOrDefault() as
                   ElementTypeDescriptionAttribute;

                if (description != null)
                {
                    elementType.IsRequiredEnabled = description.IsRequiredEnabled;
                    elementType.IsValidationEnabled = description.IsValidationEnabled;
                    elementType.IsValuesEnabled = description.IsValuesEnabled;
                    elementType.IsMaxLengthEnabled = description.IsMaxLengthEnabled;
                }

                result.Add(elementType);
            }
            return result;
        }
    }
}