using CoreShop.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace CoreShop.Auth
{
  public class AppAuthorize : AuthorizeAttribute
  {
    public AppAuthorize(AccessType type)
    {
      Roles = type == AccessType.Admin ? "ADMIN" : "USER";
    }
  }
}