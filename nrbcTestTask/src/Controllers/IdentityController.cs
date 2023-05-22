using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using nrbcTestTask.Models;
using nrbcTestTask.Models.ViewModels;

namespace nrbcTestTask.Controllers
{
	[Authorize]
	public class IdentityController : Controller
	{
		private UserManager<ApplicationUser> _userManager;
		private SignInManager<ApplicationUser> _signInManager;

		public IdentityController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager) {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Create() 
			=> View();


		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateUserModel model) {

			if (ModelState.IsValid) {

				ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);

				if (user != null) {
					ModelState.AddModelError(nameof(model.UserName), "Пользователь с таким именем уже существует");
					return View(model);
				}

				ApplicationUser appUser = new ApplicationUser {
					UserName = model.UserName
				};

				IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

				if (result.Succeeded) {
					if (model.Enabled) {
						IdentityResult resultClaim = await _userManager.AddClaimAsync(appUser,
							new System.Security.Claims.Claim("AgeLimit", "Аllowed"));
						if (!resultClaim.Succeeded) {
							foreach (IdentityError error in result.Errors) {
								ModelState.AddModelError("", error.Description);
							}
						}
					}
					return RedirectToAction("Login", "Identity");
				}
				else {
					foreach (IdentityError error in result.Errors) {
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(model);
		}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login(/*string returnUrl*/) {
			return View(new LoginUserModel { /*ReturnUrl = returnUrl ?? "/Home/Index"*/ });
		}
		

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginUserModel model/*, string returnUrl*/) {
			if (ModelState.IsValid) {
				//  find a user by name
				ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);

				if (user != null) {
					//  if the user is found, authenticate
					await _signInManager.SignOutAsync();
					// checked result authenticate 
					Microsoft.AspNetCore.Identity.SignInResult result =
							await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

					//  if checked is succeded redirect to target URL
					if (result.Succeeded) {
						return Redirect(/*returnUrl ??*/ "/Home/Index");
					}
					else {
						ModelState.AddModelError(nameof(model.Password), "Неверный пароль");
					} 
				}
				else
					//  if checked is unsucceded add error to model state
					ModelState.AddModelError(nameof(model.UserName), "Пользователь не найден");
			}

			return View(model);
		}


		public async Task<IActionResult> Logout() {
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
