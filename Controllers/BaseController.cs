using System.Linq;
using CoreShop.Helpers;
using CoreShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace CoreShop.Controllers
{
  public abstract class BaseController : Controller
  {
    protected AppDbContext context;
    public BaseController(AppDbContext context) => this.context = context;

    protected bool IsAdmin
    {
      get
      {
        return HttpContext.User.Claims.Any(p => p.Value.ToUpper() == "ADMIN");
      }
    }

    protected void ValidateModel()
    {
      if (!ModelState.IsValid)
        throw new ValidateException()
        {
          Errors = ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage)
        };
    }
  }
}