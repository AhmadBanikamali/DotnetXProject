using System.Diagnostics;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers;

public class X
{
    public string A { get; set; }
}

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    readonly HttpClient _client;

    public HomeController(ILogger<HomeController> logger,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _client = new HttpClient();
        _client.BaseAddress = new Uri("http://localhost:5114");
    }

    [Route("{a}")]
    public async Task<IActionResult> Index(X x)
    {
        const string userName = "ahmad.banikamali.normal@gmail.com";
        const string password = "8513050518@Ahmad";
        var applicationUser = new ApplicationUser()
        {
            UserName = userName
        };
        var identityResult = await _userManager.CreateAsync(applicationUser,password);

        if (identityResult.Succeeded)
        {
            var addToRoleAsync = await _userManager.AddToRoleAsync(applicationUser, "normaluser");
            if (addToRoleAsync.Succeeded)
            {
                Console.WriteLine("ok");
                var signInResult = await _signInManager.PasswordSignInAsync(applicationUser,password,false,false);
                if (signInResult.Succeeded)
                {
                    Console.WriteLine("ok sign up");
                }
            }
        }

        try
        {
            var stringAsync = await _client.GetStringAsync("WeatherForecast");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        return View();
    }


    [Authorize]
    public async Task<IActionResult> Privacy()
    {
        await _signInManager.SignOutAsync();
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    protected override void Dispose(bool disposing)
    {
        _client.Dispose();
        base.Dispose(disposing);
    }
}