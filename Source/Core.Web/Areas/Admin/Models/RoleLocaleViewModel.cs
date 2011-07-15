using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin.Models
{
    public class RoleLocaleViewModel : IMappedModel<Role, RoleLocaleViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }
        public long RoleId { get; set; }
        public String SelectedCulture { get; set; }
        public String Name { get; set; }

        public RoleLocaleViewModel MapFrom(Role from)
        {
            RoleId = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
            Name = from.Name;

            return this;
        }

        public Role MapTo(Role to)
        {
            to.Name = Name;

            return to;
        }
    }
}