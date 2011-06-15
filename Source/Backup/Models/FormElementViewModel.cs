using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Forms.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Forms.Models
{
    public class FormElementViewModel : IMappedModel<FormElement, FormElementViewModel>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        public virtual long Id { get; set; }

        public virtual String Values { get; set; }

        public virtual bool IsRequired { get; set; }

        public IEnumerable<FormElementType> Types { get; set; }

        public FormElementType Type { get; set; }

        public FormElementViewModel MapFrom(FormElement from)
        {
            Id = from.Id;
            Title = from.Title;
            return this;
        }

        public FormElement MapTo(FormElement to)
        {
            to.Id = Id;
            to.Title = Title;
            return to;
        }
    }
}