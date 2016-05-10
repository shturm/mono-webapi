using System;
using MonoWebApi.Domain;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace MonoWebApi.Infrastructure
{
	public class MyEntityMapping : ClassMap<MyEntity>
	{
		public MyEntityMapping ()
		{
			Id (x => x.Id);
			Map (x => x.Name);
			Map (x => x.Description);

			Table ("MyEntities");
		}
	}
}

