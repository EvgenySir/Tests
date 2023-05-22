namespace nrbcTestTask.Models
{
	public class DataRepository : IRepository
	{
		private ApplicationDBContext _context;
		public DataRepository(ApplicationDBContext context) 
			=> _context = context;

		public IEnumerable<ApplicationUser> Users
			=> _context.Users.OrderBy(u => u.UserName).ToArray();

		public ApplicationUser GetUserById(Guid guid)
			=> _context.Users.Where(u => u.Id == guid).FirstOrDefault();

		//public void AddUser(ApplicationUser user) { 
		//	user.Id = Guid.NewGuid();

		//	this._context.Users.Add(user);
		//	this._context.SaveChanges();
		//}
	}
}
