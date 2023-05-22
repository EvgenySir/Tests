using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace nrbcTestTask.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index() 
			=> View();


		[Authorize(Policy = "AUsers")]
		public IActionResult LimitedAccess() 
			=> View();
	}
}
