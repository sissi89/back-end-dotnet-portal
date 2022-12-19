namespace WebApi.Models;

using WebApi.Entities;

public class AuthenticateResponse
{
    public int id { get; set; }
    public int role { get; set; }
    
    public string Username { get; set; }
    public string Token { get; set; }

    public string name {get; set;}
// costruttore
    public AuthenticateResponse(User user, string token)
    {
        id = user.id;
        role = user.role;
       name = user.name;
        Username = user.Username;
        Token = token;
    }
}