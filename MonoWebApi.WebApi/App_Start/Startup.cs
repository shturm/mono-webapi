// OWIN / Security cofnig
using System;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;

// WebApi config
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using MonoWebApi.Domain;

[assembly: OwinStartup (typeof (MonoWebApi.Infrastructure.WebApi.Startup), "Configure")]

namespace MonoWebApi.Infrastructure.WebApi
{
	public class Startup
	{
		public void Configure (IAppBuilder app)
		{
			ConfigureOAuth (app);
			//ConfigureWebApi (app); // OWIN
		}

		void ConfigureOAuth(IAppBuilder app)
		{
			var oauthServerOptions = new OAuthAuthorizationServerOptions () {
				TokenEndpointPath = new PathString ("/token"),
				Provider = new ApplicationOAuthProvider ("self"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays (14),
				// In production mode set AllowInsecureHttp = false
				AllowInsecureHttp = true
			};

			app.UseOAuthBearerTokens (oauthServerOptions);
		}

		void ConfigureWebApi(IAppBuilder app)
		{
			var config = new HttpConfiguration ();
			ConfigureWebApi (config);
			app.UseWebApi (config);
		}

		public static void ConfigureWebApi(HttpConfiguration config)
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
				defaults: new { controller = "Default", action = "Get" }
			);

			var builder = new ContainerBuilder ();
			builder.RegisterApiControllers (Assembly.GetExecutingAssembly ());
			AutofacDomainConfiguration.Configure (builder);
			AutofacInfrastructureConfiguration.Configure (builder);
			var container = builder.Build ();

			config.DependencyResolver = new AutofacWebApiDependencyResolver (container);

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