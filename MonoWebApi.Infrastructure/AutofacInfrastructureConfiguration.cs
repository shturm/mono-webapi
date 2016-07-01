<<<<<<< HEAD
﻿using System;
using MonoWebApi.Domain.Infrastructure;
using Autofac;
=======
﻿using NHibernate;
using Autofac;
using MonoWebApi.Infrastructure.Services;
using MonoWebApi.Infrastructure.DataAccess;
>>>>>>> 2f5aefa604b2f1ab5bd13f7c9794fe3ce0938f8c

namespace MonoWebApi.Infrastructure
{
	public class AutofacInfrastructureConfiguration
	{
		public static void Configure(ContainerBuilder builder)
		{
			//var session = NHibernateConfiguration.OpenSession ();
			//builder.Register (c => {
			//	return NHibernateConfiguration.OpenSession ();
			//}).As <ISession>().InstancePerLifetimeScope ();

			builder.Register (c => NHibernateConfiguration.OpenSession()).As <ISession>();

			builder.RegisterType<MyInfrastructureService> ().AsImplementedInterfaces ();
		}
	}
}
