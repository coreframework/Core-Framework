namespace Core.Web.NHibernate.Models
{
    public class PageTemplate : Page
    {
        #region Constants

        public const int UnlinkOperationCode = 25;

        #endregion


        public PageTemplate()
        {
            IsTemplate = true;
        }
    }
}
