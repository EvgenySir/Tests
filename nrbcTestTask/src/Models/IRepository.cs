namespace nrbcTestTask.Models
{
	public interface IRepository
	{
		public IEnumerable<ApplicationUser> Users { get; }
		public ApplicationUser GetUserById(Guid guid);
	}
}
