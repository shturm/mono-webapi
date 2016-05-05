using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MonoWebApi.WebApi
{
	public static class WebApiConfig
	{
		public static void Register (HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes ();

			config.Routes.MapHttpRoute (
				name: "Api",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute (
				name: "Default",
				routeTemplate: "",
				defaults: new { controller = "Default", action="Get" }
			);

			//config.Formatters.Clear ();
			//config.Formatters.Add (new XmlMediaTypeFormatter ());
			//config.Formatters.Add (new JsonMediaTypeFormatter());
			//config.Formatters.Add (new FormUrlEncodedMediaTypeFormatter ());

			var settings = config.Formatters.JsonFormatter.SerializerSettings;
			settings.Formatting = Formatting.Indented;
			settings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
		}
	}
}