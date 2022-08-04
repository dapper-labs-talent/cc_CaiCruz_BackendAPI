using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth.api.Controllers;

[Authorize]
[ApiController]
public class AuthController : ControllerBase
{
    private IUserRepository UserRepo;
    private IJwtManager JwtManager;

    public AuthController(IUserRepository userRepo, IJwtManager jwtManager)
    {
        UserRepo = userRepo;
        JwtManager = jwtManager;
    }

    [AllowAnonymous]
    [HttpPost()]
    [Route("signup")]
    public async Task<ActionResult> Signup([FromBody] SignupDto signupDto)
    {
        var userDto = new UserDto
        {
            Email = signupDto.Email,
            FirstName = signupDto.FirstName,
            LastName = signupDto.LastName,
            Password = signupDto.Password
        };

        var createdUser = await UserRepo.Create(userDto);

        if (createdUser == null)
        {
            return Problem("Something went wrong creating the user");
        }

        return Ok(new { Token = JwtManager.CreateToken(createdUser) });
    }

    [AllowAnonymous]
    [HttpPost()]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var existingUser = await UserRepo.GetUserByEmail(loginDto.Email);

        if (await UserRepo.Authenticate(loginDto))
        {
            return Ok(new { Token = JwtManager.CreateToken(existingUser) });
        }
        else
        {
            return BadRequest("Invalid Credentials");
        }
    }

    [HttpGet]
    [Route("users")]
    public async Task<ActionResult<IEnumerable<object>>> Get()
    {
        var result = await UserRepo.GetUsers();

        return Ok(new { Users = result });
    }

    [HttpPut]
    [Route("users")]
    public async Task<ActionResult> Put([FromBody] UpdateUserDto updateUserDto)
    {
        var claims = User.Identity as ClaimsIdentity;
        var email = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        await UserRepo.Update(updateUserDto, email.Value);

        return Ok();
    }
}
