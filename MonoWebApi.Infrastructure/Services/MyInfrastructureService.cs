using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using MonoWebApi.Domain.Infrastructure;
using System.Collections.Generic;
using MonoWebApi.Domain.Entities;

namespace MonoWebApi.Infrastructure.Services
{
	public class MyInfrastructureService : IMyInfrastructureService
	{
		readonly ISession _session;

		public MyInfrastructureService (ISession session)
		{
			_session = session;
		}

	}
}
