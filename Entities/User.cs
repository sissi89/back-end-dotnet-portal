namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int id { get; set; }
   
   public int role {get; set;}
    public string Username { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}