using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public interface IJwtManager
{
    string CreateToken(User user);
}

public class JwtManager : IJwtManager
{

    private IConfiguration Configuration;

    public JwtManager(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var encodedSecret = System.Text.Encoding.UTF8.GetBytes(Configuration["JwtSecret"]);
        var key = new SymmetricSecurityKey(encodedSecret);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
