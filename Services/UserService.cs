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
    // legenda 1 Operatore 2 fiduciario
    private List<User> _users = new List<User>
    {
        new User { id = 1,  role=1,  Username = "Operatore", Password = "Operatore", name = "Operatore"}, // operatore sogesa
        new User { id = 2,  role=2,   Username = "CREMONA STIME SRL - ZUCCHELLI", Password = "123456", name = "Rossi" }, // fiduciario
        new User { id = 3,  role=2,   Username = "0002", Password = "123456" , name = "Bianchi"}
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

        // ritorna null 
        if (user == null) return null;

        // genera token se l autenticazione è andata a buon fine
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

 
    // generare token
    private string generateJwtToken(User user)
    {
     
     // uso libreria per la creazione del token
        var tokenHandler = new JwtSecurityTokenHandler();
        // codifica
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.id.ToString()) }),
               // token valido 7 giorni a partire dalla data di oggi 
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        // creo token 
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // genero 
        return tokenHandler.WriteToken(token);
    }

    

   

}