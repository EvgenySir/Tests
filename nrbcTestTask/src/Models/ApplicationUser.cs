using Microsoft.AspNetCore.Identity;


namespace nrbcTestTask.Models
{
	/// <summary>
	/// The application user class is based on the IdentityUser class with Guid as the identifier
	/// </summary>
	public class ApplicationUser : IdentityUser<Guid>
	{
		public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
	}
}
