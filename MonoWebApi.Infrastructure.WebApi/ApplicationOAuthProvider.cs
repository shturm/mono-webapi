using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace MonoWebApi.Infrastructure.WebApi
{
	public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
	{
		private readonly string _publicClientId;

		public ApplicationOAuthProvider (string publicClientId)
		{
			if (publicClientId == null) {
				throw new ArgumentNullException ("publicClientId");
			}

			_publicClientId = publicClientId;
		}

		public override async Task GrantResourceOwnerCredentials (OAuthGrantResourceOwnerCredentialsContext context)
		{
			var userManager = new UserManager ();
			var user = await userManager.FindAsync (context.UserName, context.Password);

			if (user== null)
			{
				context.SetError("invalid_grant", "The user name or password is incorrect.");
				return;
			}

			ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync (user, OAuthDefaults.AuthenticationType);

			AuthenticationProperties properties = CreateProperties (context.UserName);
			AuthenticationTicket ticket = new AuthenticationTicket (oAuthIdentity, properties);
			context.Validated (ticket);
			context.Request.Context.Authentication.SignIn (oAuthIdentity);

			//return Task.FromResult<object> (null);
		}

		public override Task ValidateClientAuthentication (OAuthValidateClientAuthenticationContext context)
		{
			// Resource owner password credentials does not provide a client ID.
			context.Validated ();
			return Task.FromResult<object> (null);
		}

		public override Task ValidateClientRedirectUri (OAuthValidateClientRedirectUriContext context)
		{
			//if (context.ClientId == _publicClientId)
			//{
			//    Uri expectedRootUri = new Uri(context.Request.Uri, "/");

			//    if (expectedRootUri.AbsoluteUri == context.RedirectUri)
			//    {
			//        context.Validated();
			//    }
			//}
			context.Validated ();
			return Task.FromResult<object> (null);
		}

		public static AuthenticationProperties CreateProperties (string userName)
		{
			IDictionary<string, string> data = new Dictionary<string, string>
			{
				{ "userName", userName }
			};
			return new AuthenticationProperties (data);
		}
	}
}