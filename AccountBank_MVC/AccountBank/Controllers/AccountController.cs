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
            account.Status = true;
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



    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [HttpPost]
    [Route("login")]
    public IActionResult login(string username, string password, Account account)
    {

        if (_accountService.Login(username, password))
        {

            HttpContext.Session.SetString("username", username);
            return RedirectToAction("welcome");
        }
        else
        {
            //ViewBag.msg = "Invalid";
            TempData["msg"] = "Invalid";
            return RedirectToAction("Index");
        }

    }

    [Route("welcome")]
    public IActionResult welcome()
    {
        ViewBag.username = HttpContext.Session.GetString("username");
        return View("welcome");
    }
}
