using System.Collections.Generic;
using System.Linq;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Framework.Permissions.Contracts;
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
            var form = new Form
            {
                Title = "Test form"
            };
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

        [Test]
        public void GetAllowedFormsByOperationTest()
        {
            var formService = Container.Resolve<IFormService>();

            var form = CreateForm();
            formService.Save(form);

            var permissionService = Container.Resolve<IPermissionCommonService>();
            permissionService.SetupDefaultRolePermissions(form.Operations, typeof(Form), form.Id);

            var forms = formService.GetAllowedFormsByOperation(null, (int)FormOperations.View);

            Assert.Contains(form, forms.ToList());

            formService.Delete(form);
        }

        #endregion
    }
}
