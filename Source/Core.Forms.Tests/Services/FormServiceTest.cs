using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using NUnit.Framework;

namespace Core.Forms.Tests.Services
{
    [TestFixture]
    public class FormServiceTest : AbstractServiceTest<Form, IFormService>
    {
        /// <summary>
        /// Creates the form.
        /// </summary>
        /// <returns></returns>
        public Form CreateForm()
        {
            var form = new Form();
            ((FormLocale)form.CurrentLocale).Title = "test form";
          
            return form;
        }

        #region Test Methods

        [Test]
        public void GetForm()
        {
            var formService = Container.Resolve<IFormService>();
       
            var form = CreateForm();
            formService.Save(form);

            var savedForm = formService.Find(form.Id);
            Assert.NotNull(savedForm);

            formService.Delete(form);
        }

        #endregion
    }
}
