using System;
using System.Threading;
using MonoWebApi.Infrastructure.WebApi.Controllers;

using NUnit.Framework;

namespace MonoWebApi.Infrastructure.WebApi.Tests
{
	[TestFixture]
	public class DefaultControllerTests : ApiControllerTests<DefaultController>
	{
		[Test]
		[Category ("Unit")]
		public void DefaultController_can_construct ()
		{
			Assert.AreEqual ("Hello joe", Controller.Hello ("joe"));
		}
	}
}

