using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace nrbcTestTask.Models
{
	public class ApplicationDBContext : DbContext {
		public ApplicationDBContext(DbContextOptions options) : base(options) {
		}

		public DbSet<ApplicationUser> Users { get; set; }

		public DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			//	Creating navigation properties
			modelBuilder.Entity<ApplicationUser>(b => {
				b.HasMany(e => e.Claims)
				.WithOne()
				.HasForeignKey(uc => uc.UserId)
				.IsRequired();
			});

			//	Change the name of the Users table
			modelBuilder.Entity<ApplicationUser>(b => {
				b.ToTable("Users");
			});

			//	Change the name of the Claims table
			modelBuilder.Entity<IdentityUserClaim<Guid>>(b => {
				b.ToTable("UserClaims");
			});

			//	Remove unnecessary fields in Users entity
			modelBuilder.Entity<ApplicationUser>().Ignore(c => c.AccessFailedCount)
				.Ignore(c => c.LockoutEnabled)
				.Ignore(c => c.LockoutEnd)
				.Ignore(c => c.TwoFactorEnabled)
				.Ignore(c => c.PhoneNumberConfirmed)
				.Ignore(c => c.PhoneNumber)
				.Ignore(c => c.EmailConfirmed)
				.Ignore(c => c.NormalizedEmail)
				.Ignore(c => c.Email);

			//	Change length of fields in User entity
			modelBuilder.Entity<ApplicationUser>(b => {
				b.Property(u => u.UserName).HasMaxLength(128);
				b.Property(u => u.NormalizedUserName).HasMaxLength(128);
			});
		}
	}
}
