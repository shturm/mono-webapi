using System;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace MonoWebApi.Infrastructure
{
	

	public class NHibernateConfiguration
	{
		static ISessionFactory _factory;

		public static ISessionFactory GetSessionFactory()
		{
			if (_factory != null) {
				return _factory;
			}

			_factory = Fluently.Configure ()
							   .Database (
								   MySQLConfiguration
					                   .Standard
					                   .ConnectionString ("Server=localhost;Database=koshiyam;Uid=uniuser;Pwd=unipass;")
								)
			                   .Mappings (x => x.FluentMappings.AddFromAssembly (Assembly.GetExecutingAssembly ()))
			                   .BuildSessionFactory ();

			return _factory;
		}
	}
}

