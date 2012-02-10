using System.Collections.Generic;
using System.Linq;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;
using Core.Web.NHibernate.Permissions.Operations;
using NUnit.Framework;

namespace Core.Web.Tests.Services
{
    [TestFixture]
    public class PermissionCommonServiceTest : AbstractServiceTest<Permission, IPermissionService>
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
                   user.Roles.Add(role);
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

        /// <summary>
        /// Check IsAllowed IPermissionCimmonService method
        /// </summary>
        [Test]
        public void IsPermissionActionAllowed()
        {
            //create new page

            var permissionCommonService = Container.Resolve<IPermissionCommonService>();
            var pageService = Container.Resolve<IPageService>();
            var userService = Container.Resolve<IUserService>();
            var permissionService = Container.Resolve<IPermissionService>();

            var page = CreatePage();
            pageService.Save(page);

            //check administrator rights
            var user = CreateUserAndAssignToRole((int) SystemRole.Administrator);
            userService.Save(user);

            var isAllowed = permissionCommonService.IsAllowed((int)PageOperations.View, user, typeof(Page), page.Id, PermissionOperationLevel.Object);

            Assert.AreEqual(true, isAllowed);
            userService.Delete(user);

            //check user rights
            user = CreateUserAndAssignToRole(null);
            userService.Save(user);

            var permission = SetupPermissionsForPage(page, (int) SystemRole.User);
            permissionService.Save(permission);


            isAllowed = permissionCommonService.IsAllowed((int)PageOperations.View, user, typeof(Page), page.Id, PermissionOperationLevel.Object);
            Assert.AreEqual(false, isAllowed);

            permission.Permissions = (int)PageOperations.Delete;
            permissionService.Save(permission);

            isAllowed = permissionCommonService.IsAllowed((int)PageOperations.Delete, user, typeof(Page), page.Id, PermissionOperationLevel.Object);
            Assert.AreEqual(true, isAllowed);

            userService.Delete(user);
            pageService.Delete(page);
        }

        /// <summary>
        /// Check GetAccess IPermissionCommonService method
        /// </summary>
        [Test]
        public void GetAccess()
        {
            var permissionCommonService = Container.Resolve<IPermissionCommonService>();
            var pageService = Container.Resolve<IPageService>();
            var userService = Container.Resolve<IUserService>();
            var permissionService = Container.Resolve<IPermissionService>();

            var page = CreatePage();
            pageService.Save(page);

            //check administrator rights
            var user = CreateUserAndAssignToRole((int)SystemRole.Administrator);
            userService.Save(user);

            var result = permissionCommonService.GetAccess(page.Operations,user,typeof(Page),page.Id);

            Assert.AreEqual(page.Operations.ToDictionary(value => value.Key, value => true), result);
            userService.Delete(user);

            //check user rights
            user = CreateUserAndAssignToRole(null);
            userService.Save(user);

            var permission = SetupPermissionsForPage(page, (int)SystemRole.User);
            permissionService.Save(permission);

            result = permissionCommonService.GetAccess(page.Operations, user, typeof(Page), page.Id);
            Assert.AreEqual(page.Operations.ToDictionary(value => value.Key, value => false), result);

            permissionService.Delete(permission);
            userService.Delete(user);
            pageService.Delete(page);
        }

        /// <summary>
        /// Check SetupDefaultRolePermissions IPermissionCommonService method
        /// </summary>
        [Test]
        public void SetupDefaultRolePermissions()
        {
            var permissionCommonService = Container.Resolve<IPermissionCommonService>();
            var pageService = Container.Resolve<IPageService>();
            var userService = Container.Resolve<IUserService>();
            var permissionService = Container.Resolve<IPermissionService>();

            var page = CreatePage();
            pageService.Save(page);
            var operations = GetTestObjectOperations();

            permissionCommonService.SetupDefaultRolePermissions(operations,typeof(Page),page.Id);

            //check guest
            var isAllowed = permissionCommonService.IsAllowed(1, null, typeof(Page), page.Id, PermissionOperationLevel.Object);
            Assert.IsFalse(isAllowed);
            
            //check user
            var user = CreateUserAndAssignToRole(null);
            userService.Save(user);

            isAllowed = permissionCommonService.IsAllowed(1, user, typeof(Page), page.Id, PermissionOperationLevel.Object);
            Assert.IsTrue(isAllowed);

            //check owner

            isAllowed = permissionCommonService.IsAllowed(1, user, typeof(Page), page.Id, true, PermissionOperationLevel.Object);
            Assert.IsTrue(isAllowed);

            userService.Delete(user);
            pageService.Delete(page);

            var query = from permission in permissionService.CreateQuery()
                        where permission.EntityType.Name == PermissionsHelper.GetEntityType(typeof(Page)) &&
                              permission.EntityId == page.Id
                        select permission;

            var result =  query.ToList();

            Assert.AreEqual(3, result.Count);

            foreach (var item in result)
            {
                permissionService.Delete(item);
            }

            result = query.ToList();
            Assert.AreEqual(0, result.Count);
        }
    }
}
