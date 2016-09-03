using System;
using System.Configuration;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using MonoWebApi.Infrastructure.DataAccess;
using MonoWebApi.Infrastructure;
using MonoWebApi.Infrastructure.WebApi;
using MonoWebApi.Domain;
using NHibernate;
using NUnit.Framework;
using System.Web.Http.Hosting;
using System.Web.Http;

namespace MonoWebApi.Infrastructure.WebApi.Tests
{
	[TestFixture]
	public abstract class ApiControllerTests<TController> where TController : ApiController
	{
		

		public IContainer Container { get; private set; }
		public ISession Session { get; private set; }
		public ILifetimeScope Scope { get; private set; }
		public TController Controller { get; private set; }

		[TestFixtureSetUp]
		public void Init ()
		{
			ConfigurationManager.ConnectionStrings.Add (
				new ConnectionStringSettings ("DefaultConnection", "Server=localhost;Database=koshiyam;Uid=uniuser;Pwd=unipass;")
			);
			Session = NHibernateConfiguration.OpenSession ();


			var builder = new ContainerBuilder ();
			builder.RegisterApiControllers (Assembly.GetAssembly (typeof (Startup)));
			AutofacInfrastructureConfiguration.Configure (builder);
			AutofacDomainConfiguration.Configure (builder);
			Container = builder.Build ();
		}

		[SetUp]
		public void BeforeEach ()
		{
			Scope = Container.BeginLifetimeScope ();
			try {
				Controller = Scope.Resolve<TController> ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
			Session = NHibernateConfiguration.OpenSession ();
			Controller.Request = new System.Net.Http.HttpRequestMessage ();
			Controller.Request.Properties.Add (HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration ());
		}

	}
}

