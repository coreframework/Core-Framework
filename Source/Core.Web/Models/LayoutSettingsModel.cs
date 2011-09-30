using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Core.Web.Helpers;

namespace Core.Web.Models
{
    [DataContract]
    [ModelBinder(typeof(JsonModelBinder))]
    public class LayoutSettingsModel
    {
        [DataMember]
        public long LayoutId { get; set; }
        [DataMember]
        public IList<RowSettings> RowsSetting { get; set; }
    }
}