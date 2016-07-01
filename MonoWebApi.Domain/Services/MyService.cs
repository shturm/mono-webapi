using System;
using MonoWebApi.Domain.Infrastructure;

namespace MonoWebApi.Domain
{
	public class MyService : IMyService
	{
		IMyInfrastructureService _infService { get; set; }

		public MyService (IMyInfrastructureService infService)
		{
			_infService = infService;
		}

		public string GetGreeting ()
		{
			return "Hello from the Domain";
		}
	}
}