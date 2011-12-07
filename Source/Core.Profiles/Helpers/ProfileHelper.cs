using System.Collections.Generic;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Helpers
{
    public static class ProfileHelper
    {
        /// <summary>
        /// Binds the profile element.
        /// </summary>
        /// <param name="profileTypeId">The profile type id.</param>
        /// <returns></returns>
        public static IEnumerable<ProfileHeader> BindProfileElement(long profileTypeId)
        {
            var profileHeaderService = ServiceLocator.Current.GetInstance<IProfileHeaderService>();
            var headers = profileHeaderService.GetProfileHeaders(profileTypeId);

            return headers;
        }

        /// <summary>
        /// Updates the profile elements positions.
        /// </summary>
        /// <param name="profileElementId">The profile element id.</param>
        /// <param name="profileHeaderId">The profile header id.</param>
        /// <param name="orderNumber">The order number.</param>
        public static void UpdateProfileElementsPositions(long? profileElementId, long? profileHeaderId, int orderNumber)
        {
            var profileTypeService = ServiceLocator.Current.GetInstance<IProfileTypeService>();
            var profileHeaderService = ServiceLocator.Current.GetInstance<IProfileHeaderService>();
            var profileElementService = ServiceLocator.Current.GetInstance<IProfileElementService>();

            if (profileHeaderId !=null)
            {
                ProfileHeader header = profileHeaderService.Find((long)profileHeaderId);

                if (header != null)
                {
                    header.ProfileType.ProfileHeaders.Update(
                             el =>
                             {
                                 el.OrderNumber =
                                     el.OrderNumber > header.OrderNumber
                                          ? el.OrderNumber - 1
                                          : el.OrderNumber;
                             }
                             );
                    header.ProfileType.ProfileHeaders.Update(
                        el =>
                        {
                            el.OrderNumber =
                                el.OrderNumber >= orderNumber
                                     ? el.OrderNumber + 1
                                     : el.OrderNumber;
                        }
                        );
                    profileTypeService.Save(header.ProfileType);
                    header.OrderNumber = orderNumber;
                    profileHeaderService.Save(header);
                }
            }
            else if (profileElementId !=null)
            {
                ProfileElement element = profileElementService.Find((long)profileElementId);

                if (element != null)
                {
                    element.ProfileHeader.ProfileElements.Update(
                             el =>
                             {
                                 el.OrderNumber =
                                     el.OrderNumber > element.OrderNumber
                                          ? el.OrderNumber - 1
                                          : el.OrderNumber;
                             }
                             );
                    element.ProfileHeader.ProfileElements.Update(
                        el =>
                        {
                            el.OrderNumber =
                                el.OrderNumber >= orderNumber
                                     ? el.OrderNumber + 1
                                     : el.OrderNumber;
                        }
                        );
                    profileHeaderService.Save(element.ProfileHeader);
                    element.OrderNumber = orderNumber;
                    profileElementService.Save(element);
                }
            }
        }
    }
}