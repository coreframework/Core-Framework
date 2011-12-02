using System.Collections.Generic;
using Core.Profiles.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Profiles.NHibernate.Contracts
{
    public interface IProfileHeaderService : IDataService<ProfileHeader>
    {
        IEnumerable<ProfileHeader> GetProfileHeaders(long profileId);
    }
}
