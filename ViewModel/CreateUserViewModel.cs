using System.ComponentModel.DataAnnotations;
using CoreShop.ViewModel.Validation;

namespace CoreShop.ViewModel
{
  public class CreateUserViewModel
  {
    [RequiredField("Login")]
    [MinFieldLength("Login", 5)]
    public string Login { get; set; }

    [RequiredField("Email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [RequiredField("Password")]
    [MinFieldLength("Password", 6)]
    
    public string Password { get; set; }

    [TypeUserField]
    public string Tipo { get; set; }
  }
}