using System.Collections.Generic;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Helpers
{
    public static class ProfileHelper
    {
        public static IEnumerable<ProfileElementGridModel> BindProfileElement(long profileTypeId)
        {
            var profileHeaderService = ServiceLocator.Current.GetInstance<IProfileHeaderService>();
            var headers = profileHeaderService.GetProfileHeaders(profileTypeId);

            foreach (var item in headers)
            {
                yield return new ProfileElementGridModel
                                 {
                                     Id = item.Id,
                                     OrderNumber = item.OrderNumber,
                                     Title = item.Title,
                                     Type = ProfileElementGridModelType.Header
                                 };

                foreach (var element in item.ProfileElements)
                {
                    yield return new ProfileElementGridModel
                                     {
                                         Id = element.Id,
                                         OrderNumber = element.OrderNumber,
                                         Title = element.Title,
                                         ParentId = item.Id,
                                         Type = ProfileElementGridModelType.Element
                                     };
                }
            }
        }
    }
}