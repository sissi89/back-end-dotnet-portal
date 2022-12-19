namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int id { get; set; }
   
   public int role {get; set;}
    public string Username { get; set; }

    public string name {get;set;}
 
    [JsonIgnore] // cosi la password non è visibile dalla chaiata get user
    public string Password { get; set; }
}