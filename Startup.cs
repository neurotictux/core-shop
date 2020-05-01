using CoreShop.Auth;
using CoreShop.Helpers;
using CoreShop.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoreShop
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = JwtSecurityKey.Create()
      };
      services.AddDbContext<AppDbContext>(options =>
          options.UseSqlite("DataSource=shop.db"));

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(
        options =>
        {
          options.TokenValidationParameters = tokenValidationParameters;
        }
      );

      services.AddControllers();
      services.AddRouting();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseAuthentication();
      app.UseMiddleware(typeof(ExceptionHandling));

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}