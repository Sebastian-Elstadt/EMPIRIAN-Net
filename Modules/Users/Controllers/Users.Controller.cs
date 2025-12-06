using EMPIRIAN.Modules.Users.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace EMPIRIAN.Modules.Users.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService usersService;

    public UsersController(IUsersService usersService)
    {
        this.usersService = usersService;
    }

    [HttpPost]
    [Route("phantom")]
    public async Task<IActionResult> CreatePhantomUser()
    {
        try
        {
            var user = await usersService.CreatePhantomUser();
            return Ok(new { UserID = user.ID });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}