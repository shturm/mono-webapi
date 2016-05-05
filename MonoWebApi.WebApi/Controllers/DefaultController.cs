using System;
using System.Web.Http;

namespace MonoWebApi.Infrastructure.WebApi
{
	public class DefaultController : ApiController
	{
		public IHttpActionResult Get()
		{
			return Ok (new { message = "Hello WebApi" });
		}
	}
}