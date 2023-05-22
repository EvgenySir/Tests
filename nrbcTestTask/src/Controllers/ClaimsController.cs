using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace nrbcTestTask.Controllers
{
	public class ClaimsController : Controller
	{
		[Authorize]
		public IActionResult Index()
			=> View(User?.Claims);
	}
}
