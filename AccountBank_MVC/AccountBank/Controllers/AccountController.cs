using Microsoft.AspNetCore.Mvc;

namespace AccountBank.Controllers;
[Route("account")]
public class AccountController : Controller
{
    [Route("")]
    [Route("~/")]
    [Route("index")]
    public IActionResult Index()
    {
        return View();
    }
}
