using System;

namespace Core.Profiles.Models
{
    public class ProfileElementGridModel
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public String Title { get; set; }

        public long OrderNumber { get; set; }

        public ProfileElementGridModelType Type { get; set; }

    }
}