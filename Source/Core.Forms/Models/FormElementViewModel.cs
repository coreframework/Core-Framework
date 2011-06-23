using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Forms.NHibernate.Helpers;
using Core.Forms.NHibernate.Models;
using Core.Forms.Validation;
using Framework.Core.DomainModel;

namespace Core.Forms.Models
{
    public class FormElementViewModel : IMappedModel<FormElement, FormElementViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormElementViewModel"/> class.
        /// </summary>
        public FormElementViewModel()
        {
           
        }

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
        public List<ElementTypeDescriptionModel> Types { get; set; }

        /// <summary>
        /// Gets or sets the validation regex template.
        /// </summary>
        /// <value>The validation regex template.</value>
        public RegexTemplate RegexTemplate { get; set; }

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

        public FormElementViewModel MapFrom(FormElement from)
        {
            Id = from.Id;
            Title = from.Title;
            Types = new List<ElementTypeDescriptionModel>();
            RegexTemplate = from.RegexTemplate;
            MaxLength = from.MaxLength;
            Type = from.Type;
            Values = from.ElementValues;
            IsRequired = from.IsRequired;

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
                }

                Types.Add(elementType);
            }
 
            return this;
        }

        public FormElement MapTo(FormElement to)
        {
            to.Id = Id;
            to.Title = Title;
            to.IsRequired = IsRequired;
            to.ElementValues = Values;
            to.Type = Type;
            to.RegexTemplate = RegexTemplate;
            to.MaxLength = MaxLength;
            return to;
        }
    }
}