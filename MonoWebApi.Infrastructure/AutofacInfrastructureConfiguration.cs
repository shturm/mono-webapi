using NHibernate;
using Autofac;
using MonoWebApi.Infrastructure.Services;
using MonoWebApi.Infrastructure.DataAccess;

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