using System;
using MonoWebApi.Domain.Infrastructure;
using Autofac;

namespace MonoWebApi.Infrastructure
{
	public class AutofacInfrastructureConfiguration
	{
		public static void Configure(ContainerBuilder builder)
		{
			builder.RegisterType<MyInfrastructureService> ().AsImplementedInterfaces ();
		}
	}
}

