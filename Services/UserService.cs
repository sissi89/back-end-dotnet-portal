namespace WebApi.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);
}

public class UserService : IUserService
{
    // database fake
    private List<User> _users = new List<User>
    {
        new User { id = 1, role =1,  Username = "Operatore", Password = "Operatore" }, // operatore sogesa
        new User { id = 2,  role=2,   Username = "0001", Password = "123456" }, // fiduciario
        new User { id = 3,  role=2,   Username = "0002", Password = "123456" }
    };

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    // post autenticazione
    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        // console.log
        System.Console.Write("scrivo in console");
        return _users;
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(x => x.id == id);
    }

    // helper methods

    private string generateJwtToken(User user)
    {
     
        var tokenHandler = new JwtSecurityTokenHandler();
        // key
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        System.Console.WriteLine("key"+key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.id.ToString()) }),
               // token valido 7 giorni
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    

   

}