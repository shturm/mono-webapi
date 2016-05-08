using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MonoWebApi.Infrastructure.WebApi.Controllers
{
	
	using UserManager = MonoWebApi.Infrastructure.UserManager;

	public class AccountsController : ApiController
	{
		//UserManager _userManager;

		//public AccountsController (UserManager userManager)
		//{
		//	_userManager = userManager;
		//}

		public IHttpActionResult Get ()
		{
			
			return Ok (new {
				UserName = User.Identity.Name,
				UserId = User.Identity.GetUserId (),
				IsAuthenticated= User.Identity.IsAuthenticated,
				AuthenticationType= User.Identity.AuthenticationType,
			});
		}

		[HttpPost]
		public async Task<IHttpActionResult> Register ([FromBody]RegisterCommand registerCommand)
		{
			if (!ModelState.IsValid) {
				return BadRequest (ModelState);
			}

			var userManager = new UserManager ();
			var user = new IdentityUser (registerCommand.Email);
			var identityResult = await userManager.CreateAsync (user, registerCommand.Password);



			if (!identityResult.Succeeded) {
				return GetErrorResult (identityResult);
			}

			return Ok (new { message = "Success" });
		}

		private IHttpActionResult GetErrorResult (IdentityResult result)
		{
			if (result == null) {
				return InternalServerError ();
			}

			if (!result.Succeeded) {
				if (result.Errors != null) {
					foreach (string error in result.Errors) {
						ModelState.AddModelError ("", error);
					}
				}

				if (ModelState.IsValid) {
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest ();
				}

				return BadRequest (ModelState);
			}

			return null;
		}
	}
}