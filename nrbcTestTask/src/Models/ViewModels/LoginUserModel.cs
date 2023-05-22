using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace nrbcTestTask.Models.ViewModels
{
	public class LoginUserModel
	{
		[Display(Name = "Имя пользователя")]
		[Required(ErrorMessage = "Пожалуйста ведите {0}")]
		[StringLength(128,
			ErrorMessage = "{0} длина имени пользователя должна быть не менее {1} и не более {1} символов",
			MinimumLength = 4)]
		public string UserName { get; set; }


		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Пожалуйста введите {0}")]
		[UIHint("пароль")]
		[MinLength(6, ErrorMessage = "{0} должен быть больше чем {1} символов")]
		public string Password { get; set; }


		//  Return URL
		//public string ReturnUrl { get; set; }
	}
}
