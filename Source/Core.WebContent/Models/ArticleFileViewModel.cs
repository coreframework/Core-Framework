using System;
using System.ComponentModel.DataAnnotations;
using Core.WebContent.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Mvc.Metadata.Attributes;

namespace Core.WebContent.Models
{
    public class ArticleFileViewModel : IMappedModel<ArticleFile, ArticleFileViewModel>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }
     
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("ImageUpload")]
        [ImageUpload(Resize = true, ResizeWidth = 120, ResizeHeight = 100)]
        [Required]
        public virtual String FileName { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        /// <value>The category id.</value>
        [Required]
        public long ArticleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow manage].
        /// </summary>
        /// <value><c>true</c> if [allow manage]; otherwise, <c>false</c>.</value>
        public bool AllowManage { get; set; }

        public ArticleFileViewModel MapFrom(ArticleFile articleFile)
        {
            Id = articleFile.Id;
            Title = articleFile.Title;
            FileName = articleFile.FileName;
            ArticleId = articleFile.Article != null ? articleFile.Article.Id : 0;
            return this;
        }

        public ArticleFile MapTo(ArticleFile to)
        {
            to.Id = Id;
            to.Article = new Article() { Id = ArticleId };
            to.Title = Title;
            to.FileName = FileName;
            return to;
        }
    }
}