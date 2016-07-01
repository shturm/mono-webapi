using System;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;
using System.Configuration;
using NHibernate.Cfg;

namespace MonoWebApi.Infrastructure.DataAccess
{
	public class NHibernateConfiguration
	{
		static ISessionFactory _factory;
		public static readonly object lockObject = new object ();
		public static FluentConfiguration FNHConfiguration { get; private set;}

		public static NHibernate.Cfg.Configuration NHConfiguration { get; private set; }


		public static ISessionFactory GetSessionFactory ()
		{
			if (_factory != null) {
				return _factory;
			}

			lock (lockObject) {
				var connectionString = ConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString;
				var configuration = Fluently.Configure ()
				   .Database (MySQLConfiguration.Standard.ConnectionString (connectionString))
					//.Mappings (x => {
					//	x.FluentMappings.AddFromAssembly (Assembly.GetExecutingAssembly ());
					//	//x.FluentMappings.Conventions.Add <NHM2MTableNameConvention>();
					//});
					.Mappings (x => x.FluentMappings.AddFromAssembly (Assembly.GetExecutingAssembly ()))
				                            .ExposeConfiguration (SetNHConfiguration);
				FNHConfiguration = configuration;
				_factory = configuration.BuildSessionFactory ();
			}

			return _factory;
		}

		public static ISession OpenSession ()
		{
			var factory = GetSessionFactory ();
			var session = factory.OpenSession ();

			return session;
		}

		static void SetNHConfiguration (NHibernate.Cfg.Configuration config)
		{
			NHConfiguration = config;
		}
	}
}