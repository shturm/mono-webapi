using System;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;

[assembly: OwinStartup (typeof (MonoWebApi.Infrastructure.WebApi.Startup), "Configure")]

namespace MonoWebApi.Infrastructure.WebApi
{
	public class Startup
	{
		public void Configure (IAppBuilder app)
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
	}
}