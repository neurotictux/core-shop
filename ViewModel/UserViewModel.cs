using System.ComponentModel.DataAnnotations;
using CoreShop.Infra.Models;
using CoreShop.ViewModel.Validation;

namespace CoreShop.ViewModel
{
  public class UserViewModel
  {
    public UserViewModel() { }

    public UserViewModel(Usuario u)
    {
      Id = u.Id;
      Email = u.Email;
      Login = u.Login;
      Tipo = u.Tipo;
    }
    public int Id { get; set; }

    [RequiredField("Login")]
    [MinFieldLength("Login", 5)]
    public string Login { get; set; }

    [RequiredField("Email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [TypeUserField]
    public string Tipo { get; set; }
  }
}