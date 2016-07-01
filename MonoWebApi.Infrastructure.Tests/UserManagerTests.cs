using NUnit.Framework;
using System.Configuration;
using MonoWebApi.Infrastructure;
using System.Collections.Generic;
using System;

namespace MonoWebApi.Infrastructure.Tests
{
	using UserManager = MonoWebApi.Infrastructure.UserManager;

	[TestFixture]
	public class UserManagerTests
	{
		[SetUp]
		public void SetUp()
		{
			ConfigurationManager.ConnectionStrings.Add (
				new ConnectionStringSettings ("DefaultConnection", "Server=localhost;Database=koshiyam;Uid=uniuser;Pwd=unipass;")
			);
			var db = new MySQLDatabase ();
			//db.Execute ("delete from Users", new Dictionary<string, object> ());
		}

		[Test]
		[Category("Integration")]
		public void CanInstantinate ()
		{
			Assert.DoesNotThrow (() => {
				var sut = new UserManager ();
				Assert.IsNotNull (sut);
			});
		}

		[Test]
		[Category ("Integration")]
		public async void CreatesFindsAndDeletes()
		{
			var username = "myuser";
			var user = new IdentityUser (username);
			Console.WriteLine ("GUID: {0}", user.Id);
			var password = "123456";
			var sut = new UserManager ();

			await sut.CreateAsync (user, password);
			var actualUser = await sut.FindAsync (username, password);

			Assert.IsNotNull (actualUser);
			Assert.AreEqual (user.Id, actualUser.Id);

			await sut.DeleteAsync (user);
		}

		[Test]
		[Category ("Integration")]
		public void FindAsync_WhenNoUsers_ReturnsNull()
		{
			var sut = new UserManager ();
			IdentityUser user = new IdentityUser ("NonNullUser");
			Assert.DoesNotThrow (async () => {
				user = await sut.FindAsync ("nonexistentuser", "wrongpassword");
			});
			Assert.IsNull (user);
		}
	}
}