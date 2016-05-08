using System.Web;
using System.Web.Http;

namespace MonoWebApi.Infrastructure.WebApi
{
	public class Global : HttpApplication
	{
		protected void Application_Start ()
		{
			GlobalConfiguration.Configure (Startup.ConfigureWebApi);
		}
	}
}
