using AccountBank.Models;
using AccountBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountBank.Controllers;
[Route("account")]
public class AccountController : Controller
{
    private AccountService _accountService;
    public AccountController (AccountService accountService)
    {
        _accountService = accountService;
    }

    [Route("")]
    [Route("~/")]
    [Route("index")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("register")]
    public IActionResult Register()
    {
        var account = new Account();
        return View("Register", account);
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(Account account)
    {
        if (account.Username != null && _accountService.Exists(account.Username))
        {
            ModelState.AddModelError("Username", "Username was existed");
        }
        if (ModelState.IsValid)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            if (_accountService.Create(account))
            {

                TempData["msg"] = "done";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["msg"] = "failed";
                return RedirectToAction("Register");
            }

        }
        else
        {
            return View("Register");
        }

    }




    [Route("login")]
    public IActionResult Login()
    {
        return View("Login");
    }
}
