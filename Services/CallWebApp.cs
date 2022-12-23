
using WebApi.Models;

namespace WebApi.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
//namespace httpjson.sample.api.Clients;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net.Http.Headers;
public class CallWebApp {
        public List<SinistriModel> _sinistriNode = new List<SinistriModel> { };
    public async Task<List<SinistriModel>> call(string url) {
         HttpClient client = new HttpClient();
         client.DefaultRequestHeaders.Accept.Clear();
        // gli dico che deve accettare file json 
        client.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/json")
        );
        try
        {
            //  System.Console.WriteLine("sono nel try catch");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            // mi salvo tutti i dati in una stringa 
            string data = await response.Content.ReadAsStringAsync();
            // converto in json
            var json = JsonConvert.DeserializeObject<List<SinistriModel>>(data);
            for (int i = 0; i < json.Count; i++)
            {
                _sinistriNode.Add(json[i]);
            }
            return _sinistriNode;
        }
        catch (Exception e)
        {

            System.Console.WriteLine("message di errore " + e);
            return null;

        }




    }
}