using System;
using System.Web.Http;
using MonoWebApi.Domain;

namespace MonoWebApi.Infrastructure.WebApi.Controllers
{
	public class DefaultController : ApiController
	{
		IMyService _myService;

		public DefaultController (IMyService myService)
		{
			_myService = myService;
		}
		public IHttpActionResult Get()
		{
			return Ok (new { message = "Hello WebApi", serivceMessage= _myService.GetGreeting () });
		}
	}
}