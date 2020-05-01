using System.Linq;
using CoreShop.Auth;
using CoreShop.Helpers;
using CoreShop.Infra;
using CoreShop.Infra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreShop.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class CategoriaController : BaseController
  {
    public CategoriaController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get() => Ok(context.Categoria.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id) => Ok(context.Categoria.FirstOrDefault(p => p.Id == id));

    [AppAuthorize(AccessType.Admin)]
    [HttpPost]
    public void Post([FromBody] Categoria categoria)
    {
      if (string.IsNullOrWhiteSpace(categoria.Nome))
        throw new ValidateException("A nova categoria precisa de um nome.");
      if (context.Categoria.FirstOrDefault(p => p.Nome == categoria.Nome) != null)
        throw new ValidateException("Já existe uma categoria com este nome.");
      context.Categoria.Add(new Categoria { Nome = categoria.Nome });
      context.SaveChanges();
    }

    [AppAuthorize(AccessType.Admin)]
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      if (context.Produto.FirstOrDefault(p => p.Categoria.Id == id) != null)
        throw new ValidateException("Existem produtos utilizando esta categoria.");
      var c = context.Categoria.FirstOrDefault(p => p.Id == id);
      context.Categoria.Remove(c);
      context.SaveChanges();
    }
  }
}