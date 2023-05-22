using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace nrbcTestTask.Infrastructure
{
	public class LocationClaimsProvider : IClaimsTransformation
	{
		public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal) {
			if (principal != null && !principal.HasClaim(c => 
				c.Type == "AgeLimit")) {
				ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
				if (identity != null && identity.IsAuthenticated && identity.Name != null) {
					if (identity.Name.ToLower() == "extuser18") {
						identity.AddClaim(new Claim("AgeLimit", "18+", ClaimValueTypes.String, "RemoteClaims"));
					}
				}
			}
			return Task.FromResult(principal);
		}
	}
}
