using System.Collections.Generic;
using System.Linq;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;
using NUnit.Framework;

namespace Core.Web.Tests.Services
{
    [TestFixture]
    public class PermissionServiceTest : AbstractServiceTest<Permission, IPermissionService>
    {
        /// <summary>
        /// Creates the page.
        /// </summary>
        /// <returns></returns>
        public Page CreatePage()
        {
            var page = new Page
                           {
                               Title = "Test page",
                               Url = "test-page-url"
                           };
            return page;
        }

        /// <summary>
        /// Creates the user and assign to role.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns></returns>
        public User CreateUserAndAssignToRole(long? roleId)
        {
            var user = new User
                           {
                               Email = "test@test.com",
                               Username = "test",
                               Status = UserStatus.Active,
                           };

           if (roleId!=null)
           {
               var role = Container.Resolve<IRoleService>().Find((int)roleId);
               if (role!=null)
                   user.Roles = new List<Role> {role};
           }
                
            Container.Resolve<IUserService>().SetPassword(user, "123456");

            return user;
        }

        /// <summary>
        /// Setups the permissions for page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="roleId">The role id.</param>
        /// <returns></returns>
        public Permission SetupPermissionsForPage(Page page,long roleId)
        {
            var permission = new Permission
                                 {
                                     EntityId = page.Id,
                                     EntityType = Container.Resolve<IEntityTypeService>().GetByType(typeof (Page)),
                                     Role = {Id = roleId}
                                 };

            return permission;
        }

        /// <summary>
        /// Gets the test object operations.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPermissionOperation> GetTestObjectOperations()
        {
            var operations = new List<PermissionOperation>
                                 {
                                     new PermissionOperation
                                         {
                                             GuestDefaultAcess = false,
                                             UserDefaultAccess = true,
                                             OwnerDefaultAcess = false,
                                             Area = PermissionArea.Portal,
                                             Key = 1,
                                             OperationLevel = PermissionOperationLevel.Object,
                                             Title = "Test operation"
                                         }
                                 };
            return operations;
        }

        #region Test Methods

        [Test]
        public void GetPermissionTest()
        {
            var pageService = Container.Resolve<IPageService>();
            var permissionService = Container.Resolve<IPermissionService>();

            var page = CreatePage();
            pageService.Save(page);

            var permission = SetupPermissionsForPage(page, (int)SystemRoles.User);
            permissionService.Save(permission);

            var resultPermission = permissionService.GetPermission(permission.Role.Id, permission.EntityType.Id, page.Id);

            Assert.AreEqual(permission,resultPermission,"Permission should be equal to created permission.");

            permissionService.Delete(permission);
            pageService.Delete(page);
        }

        [Test]
        public void GetResourcePermissionsTest()
        {
            var pageService = Container.Resolve<IPageService>();
            var permissionService = Container.Resolve<IPermissionService>();

            var page = CreatePage();
            pageService.Save(page);

            var permission1 = SetupPermissionsForPage(page, (int)SystemRoles.User);
            permissionService.Save(permission1);

            var permission2 = SetupPermissionsForPage(page, (int)SystemRoles.Guest);
            permissionService.Save(permission2);

            var resultPermissions = permissionService.GetResourcePermissions(typeof(Page), page.Id, false).ToList();

            Assert.Contains(permission1, resultPermissions, "Result should contains permission1 object");
            Assert.Contains(permission2, resultPermissions, "Result should contains permission2 object");

            permissionService.Delete(permission1);
            permissionService.Delete(permission2);
            pageService.Delete(page);
        }

        #endregion
    }
}
