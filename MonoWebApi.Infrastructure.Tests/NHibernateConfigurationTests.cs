using System;
using NUnit.Framework;
using System.Configuration;
using NHibernate;
using FluentNHibernate;
using MonoWebApi.Domain;

namespace MonoWebApi.Infrastructure.Tests
{
	[TestFixture]
	public class NHibernateConfigurationTests
	{
		[SetUp]
		public void SetUp()
		{
			//ConfigurationManager.ConnectionStrings.Add ();
			foreach(ConnectionStringSettings str in ConfigurationManager.ConnectionStrings)
			{
				Console.WriteLine ("{0}: {1}", str.Name, str.ConnectionString);
			}

		}

		[Test]
		[Category ("Integration")]
		public void NHibernateInfrastructureConfiguration_ConnectsToDatabase()
		{
			var factory = NHibernateConfiguration.GetSessionFactory ();

			using(var session = factory.OpenSession ())
			using (var tx = session.BeginTransaction ())
			{
				var criteria = session.CreateCriteria<MyEntity> ();
				var entities = criteria.List<MyEntity> ();
				foreach (var entity in entities) {
					Console.WriteLine ("#{0} {1} {2}", entity.Id, entity.Name, entity.Description);
				}
			}
		}
	}
}