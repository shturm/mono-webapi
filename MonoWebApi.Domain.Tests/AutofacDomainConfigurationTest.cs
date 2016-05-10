using NUnit.Framework;
using System;
using MonoWebApi.Infrastructure;
using Autofac;

namespace MonoWebApi.Domain.Tests
{
	[TestFixture ()]
	public class AutofacDomainConfigurationTest
	{
		[Test ()]
		public void AutofacDomainConfiguration_MyService_CanBeInstantinated ()
		{
			MyService resultServiceInstance;
			var builder = new ContainerBuilder ();
			AutofacDomainConfiguration.Configure (builder);
			AutofacInfrastructureConfiguration.Configure (builder);
			var container = builder.Build ();


			Assert.DoesNotThrow (() => {
				//resultServiceInstance = container.Resolve<MyService> ();
				using(var scope = container.BeginLifetimeScope ())
				{
					resultServiceInstance = scope.Resolve<IMyService> () as MyService;
					Assert.IsTrue (scope.IsRegistered<IMyService> (), "IMyService is registered");
					Assert.IsNotNull (resultServiceInstance, "MyService is initialized");
				}
			});
		}

	}
}

