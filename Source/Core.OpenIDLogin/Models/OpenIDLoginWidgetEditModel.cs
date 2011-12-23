using System;
using Core.OpenIDLogin.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.OpenIDLogin.Models
{
    public class OpenIDLoginWidgetEditModel : IMappedModel<OpenIDLoginWidget, OpenIDLoginWidgetEditModel>
    {
        #region Properties

        public long Id { get; set; }

        public bool ShowTitle { get; set; }

        public String ShowTitleFieldName { get; set; }

        public bool ChildView { get; set; }

        #endregion

        #region Constructors

        public OpenIDLoginWidgetEditModel()
        {
            ShowTitle = true;
        }

        #endregion

        #region Methods

        public OpenIDLoginWidgetEditModel MapFrom(OpenIDLoginWidget from)
        {
            Id = from.Id;
            ShowTitle = from.ShowTitle;

            return this;
        }

        public OpenIDLoginWidget MapTo(OpenIDLoginWidget to)
        {
            to.Id = Id;
            to.ShowTitle = ShowTitle;

            return to;
        }

        #endregion
    }
}