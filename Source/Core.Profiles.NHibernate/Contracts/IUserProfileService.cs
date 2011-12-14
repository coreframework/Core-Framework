using Core.Framework.Permissions.Models;
using Core.Profiles.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Profiles.NHibernate.Contracts
{
    public interface IUserProfileService : IDataService<UserProfile>
    {
        UserProfile GetUserProfile(ICorePrincipal user);
    }
}
