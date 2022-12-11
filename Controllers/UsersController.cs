namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
//[ActivatorUtilitiesConstructor] 
public class UsersController : Controller
{
    private IUserService _userService;
    private ISinistriService _sinistriService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
      
    }
    /*  public UsersController(ISinistriService sinistriService)
    {
        _sinistriService = sinistriService;
      
    } */

    [HttpPost("authenticate")]
    // autenticazione

    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    
    [HttpGet]
    // get tutti gli user
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
  


}
