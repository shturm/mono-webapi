using System;
namespace MonoWebApi.Domain
{
	public class MyService : IMyService
	{
		public string GetGreeting ()
		{
			return "Hello from the Domain";
		}
	}
}

