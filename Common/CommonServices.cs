using System.Reflection;
using Application.Auth.Common;
using Application.Common;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DbContexts;

namespace Common;

public static class CommonServices
{
    public static WebApplicationBuilder AddMyServices(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<JwtHandler>();

        builder.Services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(Assembly.Load(new AssemblyName(nameof(Application)))));


        builder.Services.AddAutoMapper(typeof(ProductMapper));

//Adding DB Context with MSSQL
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
        return builder;
    }
}