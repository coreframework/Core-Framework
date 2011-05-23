using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using NUnit.Framework;

namespace Core.Web.Tests.Services
{
    /// <summary>
    /// Tests <see cref="IUserService"/> implementation.
    /// </summary>
    [TestFixture]
    public class UserServiceTest : AbstractServiceTest<User, IUserService>
    {
        #region AbstractServiceTest<User,IUserService> members

        /// <summary>
        /// Creates the instance of <see cref="User"/> and fills it with valid details.
        /// </summary>
        /// <returns>Valid <see cref="User"/> instance.</returns>
        public User CreateInstance()
        {
            var user = new User { Username = "v.pupkin", Email = "v.pupkin@gmail.com" };
            Container.Resolve<IUserService>().SetPassword(user, "qqqqqq");
            return user;
        }

        #endregion

        #region Test methods

        /// <summary>
        /// Users should be deleted from database.
        /// </summary>
        [Test]
        public void EntityDeleteTest()
        {
            var service = Container.Resolve<IUserService>();
            var user = CreateInstance();

            service.Save(user);
            service.Delete(user);
            var userFromDatabase = service.Find(user.Id);

            Assert.IsNull(userFromDatabase, "Users should be deleted from database.");
        }

        /// <summary>
        /// Service should detect email uniqueness.
        /// </summary>
        [Test]
        public void EmailUniqueTest()
        {
            var service = Container.Resolve<IUserService>();
            var user = CreateInstance();
            var anotherUser = CreateInstance();
            service.Save(user);
            Assert.IsFalse(service.IsEmailUnique(anotherUser.Id, anotherUser.Email), "User with same email should already exist.");
            anotherUser.Email = "some.unique@email.com";
            Assert.IsTrue(service.IsEmailUnique(anotherUser.Id, anotherUser.Email), "User with same email should not already exist.");
        }

        /// <summary>
        /// Service should detect username uniqueness.
        /// </summary>
        [Test]
        public void UsernameUniqueTest()
        {
            var service = Container.Resolve<IUserService>();
            var user = CreateInstance();
            var anotherUser = CreateInstance();
            service.Save(user);
            Assert.IsFalse(service.IsUsernameUnique(anotherUser.Id, anotherUser.Username), "User with same username should already exist.");
            anotherUser.Username = "some.unique.username";
            Assert.IsTrue(service.IsUsernameUnique(anotherUser.Id, anotherUser.Username), "User with same username should not already exist.");
        }

        /// <summary>
        /// User should be found by email.
        /// </summary>
        [Test]
        public void FindByEmailTest()
        {
            var service = Container.Resolve<IUserService>();
            var user = CreateInstance();
            user.Email = "some.unique@email.com";
            service.Save(user);
            var userFromDatabase = service.FindByEmail("some.unique@email.com");

            Assert.IsNotNull(userFromDatabase, "User should be found by email.");
            Assert.AreEqual(user, userFromDatabase, "User should be equal to origin user.");
        }

        /// <summary>
        /// User should be found by username.
        /// </summary>
        [Test]
        public void FindByUsernameTest()
        {
            var service = Container.Resolve<IUserService>();
            var user = CreateInstance();
            user.Username = "some.login";
            service.Save(user);
            var userFromDatabase = service.FindByUsername("some.login");

            Assert.IsNotNull(userFromDatabase, "User should be found by username.");
            Assert.AreEqual(user, userFromDatabase, "User should be equal to origin user.");
        }

        /// <summary>
        /// User should be found by email or username.
        /// </summary>
        [Test]
        public void FindByEmailOrUsernameTest()
        {
            var service = Container.Resolve<IUserService>();
            var user = CreateInstance();
            user.Email = "some.unique@email.com";
            user.Username = "some.login";
            service.Save(user);
            Assert.IsNotNull(service.FindByEmailOrUsername("some.unique@email.com"), "User should be found by email.");
            Assert.IsNotNull(service.FindByEmailOrUsername("some.login"), "User should be found by username.");
            Assert.IsNull(service.FindByEmailOrUsername(null), "User should not be found by empty email and username.");
        }

        /// <summary>
        /// Encrypted password should pass validation.
        /// </summary>
        [Test]
        public void EncryptPasswordTest()
        {
            var service = Container.Resolve<IUserService>();
            var user = CreateInstance();

            service.SetPassword(user, "qqqqqq");

            Assert.IsNotEmpty(user.Hash);
            Assert.IsNotEmpty(user.Salt);
            Assert.AreNotEqual("qqqqqq", user.Hash);
            Assert.IsTrue(service.VerifyPassword(user, "qqqqqq"));
        }

        #endregion
    }
}