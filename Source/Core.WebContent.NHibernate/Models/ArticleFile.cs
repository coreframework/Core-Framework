using System;
using FluentNHibernate.Data;

namespace Core.WebContent.NHibernate.Models
{
    public class ArticleFile: Entity
    {
        public virtual String Title { get; set; }

        public virtual String FileName { get; set; }

        public virtual Article Article { get; set; }
    }
}
