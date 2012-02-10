using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Models
{
    public class RegistrationWidgetEditModel : IMappedModel<RegistrationWidget, RegistrationWidgetEditModel>
    {
        #region Fields

        private IEnumerable<ProfileType> profileTypes;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the profile type id.
        /// </summary>
        /// <value>The profile type id.</value>
        [Required]
        public long ProfileTypeId { get; set; }

        /// <summary>
        /// Gets the profile types.
        /// </summary>
        /// <value>The profile types.</value>
        public IEnumerable<ProfileType> ProfileTypes
        {
            get
            {
                if (profileTypes == null)
                {
                    var profileTypeService = ServiceLocator.Current.GetInstance<IProfileTypeService>();
                    profileTypes = profileTypeService.GetAll();
                }
                return profileTypes;
            }
        }

        #endregion

        public RegistrationWidgetEditModel MapFrom(RegistrationWidget from)
        {
            Id = from.Id;
            ProfileTypeId = from.ProfileType != null ? from.ProfileType.Id : 0;

            return this;
        }

        public RegistrationWidget MapTo(RegistrationWidget to)
        {
            to.Id = Id;
            to.ProfileType = new ProfileType { Id = ProfileTypeId };

            return to;
        }
    }
}