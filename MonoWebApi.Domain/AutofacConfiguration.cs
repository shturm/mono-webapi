using System;
using System.Reflection;
using Autofac;

namespace MonoWebApi.Domain
{
	public class AutofacConfiguration
	{
		public static void Configure(ContainerBuilder builder)
		{
			Assembly assembly = Assembly.GetExecutingAssembly ();

			builder.RegisterType<MyService> ().AsImplementedInterfaces ();
		}
	}
}

