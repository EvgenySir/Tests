using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Security.Claims;
using nrbcTestTask.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using nrbcTestTask.Infrastructure;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);



//  dependency implementation between the interface and the class that implements the interface
//  service is created each time it is needed(Transient)
builder.Services.AddTransient<IRepository, DataRepository>();

//	service emulating an external resource with Claims
builder.Services.AddSingleton<IClaimsTransformation, LocationClaimsProvider>();

//	policy for users with access to 18+ resources
//	the policy has 2 Claims options:
//		"18+" for emulating Claims from external resources;
//		"Allowed", for users with locally stored Claims.
builder.Services.AddAuthorization(options => {
	options.AddPolicy("AUsers", policy => {
		policy.RequireClaim("AgeLimit", new string[] { "18+", "Àllowed" });
	});
});


UserSecretsConfigurationExtensions.AddUserSecrets(builder.Configuration,
	"b93e0fcd-960f-49dc-b0df-d189a0e3d9fb");

//	add services to the container
//	2 var: UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
builder.Services.AddDbContext<ApplicationDBContext>(options =>
	options.UseSqlServer(builder.Configuration["NRBCTestTask:ConnectionString"])
);

//	add Identity
builder.Services.AddIdentityCore<ApplicationUser>()
	.AddEntityFrameworkStores<ApplicationDBContext>()
	.AddDefaultTokenProviders()
	.AddSignInManager<SignInManager<ApplicationUser>>();

//	setting up an authorization service
builder.Services.AddAuthentication(options => {
	options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
	options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
	options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, o => {
	o.LoginPath = new PathString("/Identity/Login");
	o.Events = new CookieAuthenticationEvents() {
		OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
	};
}).AddCookie(IdentityConstants.ExternalScheme, o => {
	o.Cookie.Name = IdentityConstants.ExternalScheme;
	o.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
}).AddCookie(IdentityConstants.TwoFactorRememberMeScheme, o =>
	o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme
).AddCookie(IdentityConstants.TwoFactorUserIdScheme, o => {
	o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
	o.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
});

builder.Services.AddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();

builder.Services.AddMvc();

var app = builder.Build();


// configure the http request pipeline
if (app.Environment.IsDevelopment()) { 

	app.UseDeveloperExceptionPage();
	app.UseStatusCodePages();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}"
);

app.Run();
