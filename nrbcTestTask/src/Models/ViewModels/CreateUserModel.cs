using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace nrbcTestTask.Models.ViewModels
{
	public class CreateUserModel
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


		[Display(Name = "Подтверждение пароля")]
		[Required(ErrorMessage = "Повторите пожалуйста {0}")]
		[UIHint("подтверждение пароля")]
		[MinLength(6, ErrorMessage = "{0} должен быть больше чем {1} символов")]
		[Compare("Password", ErrorMessage = "Введенный пароль и {0} не совпадают")]
		public string ConfirmPassword { get; set; }


		[Display(Name = "Подтверждение 18+")]
		public bool Enabled { get; set; }
	}
}
