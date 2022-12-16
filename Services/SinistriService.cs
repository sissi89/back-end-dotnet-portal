
namespace WebApi.Services;


// import
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

public interface ISinistriService
{
    IEnumerable<SinistriModel> GetAll();
    //public Task<List<SinistriModel>> GetAll();
    //  IEnumerable<SinistriModel> GetSinistriByFiduciario(SinistroRequest username);
    Task<List<SinistriModel>> GetSinistriByFiduciario(SinistroRequest username);

    Task<SinistriModel> GetSinistroID(SinistroRequest request);
    Task<List<SinistriModel>> Index();
    Task<SinistriModel> GetPraticalDetail2(SinistroRequest body);
}
public class SinistriService : ISinistriService
{

    public List<SinistriModel> _sinistriNode = new List<SinistriModel> { };
    // dichiaro client che mi serve per fare la fetch dei dati
    static HttpClient client = new HttpClient();
    // parametri sorta di database dichiarato come un array list  array da sostituire
    string url = "http://localhost:3000/sinistri";


    private List<SinistriModel> sinistriFetch = new List<SinistriModel> { };
    private readonly AppSettings _appSettings;

    public SinistriService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
    // tutti i sinistri con ruolo operatore
    public IEnumerable<SinistriModel> GetAll()
    //public  async Task<List<SinistriModel>> GetAll()
    {

        // console.log

        System.Console.WriteLine(_sinistriNode.Count);
        System.Console.WriteLine(_sinistriNode + " " + "sinistri node ");
        return _sinistriNode;
    }


    // sinistro by id   [HttpPost("sinistro")]
    public async Task<SinistriModel> GetSinistroID(SinistroRequest request)

    {
        System.Console.WriteLine("sinistri by fiduciario" + request.id);
        request.username = null;
        List<SinistriModel> sinistri = await Index();
        System.Console.WriteLine("i sinistri sono " + sinistri.Count);
        SinistriModel sinistro = sinistri.FirstOrDefault(x => x.id == request.id);

        System.Console.WriteLine("sinistro service" + sinistro.ToString());

        return sinistro;

    }

    // sinistri by username     [HttpPost("fiduciario")]
    public async Task<List<SinistriModel>> GetSinistriByFiduciario(SinistroRequest username)
    {

        //List<int> termsList = new List<int>(); /7 array list
        //   List<SinistriModel> sinistribyUsername = new List<SinistriModel>();
        // essendo che la classe Sinistro Request accetta 2 valori metto il valore che non serve a null
        username.id = null;

        List<SinistriModel> sinistri = await Index();
        System.Console.WriteLine("sinistri by fiduciario tutti i sinistri");
        return sinistri.FindAll(item => item.fiduciario == username.username);






    }

    // sinistro by username e id  [HttpPost("sinistroFiduciario")]

    public async Task<SinistriModel> GetPraticalDetail2(SinistroRequest body)
    {

        System.Console.WriteLine("id: sono nell getprtica controller  ");
        // mi prendo tutti i sinistri 
        List<SinistriModel> sinistri = await Index();
        System.Console.WriteLine("sinistri  " + sinistri + " " + sinistri.Count);
        // System.Console.WriteLine("sinistro service"+sinistro);
        List<SinistriModel> sinistriFiduciario = sinistri.FindAll(item => item.fiduciario == body.username);

        SinistriModel sinistro = sinistriFiduciario.FirstOrDefault(x => x.id == body.id);
        return sinistro;
        //return sinistro;


    }
    // all sinistri chiamata 

    public async Task<List<SinistriModel>> Index()
    //// client.BaseAddress va dichairato solo una volta 
    {
        if (client.BaseAddress == null)
        {
            System.Console.WriteLine("sono nell if " + client.BaseAddress);
            client.BaseAddress = new Uri(url);
           return  await getSinistri();

            
        }
        else
        {
            System.Console.WriteLine("sono nell else " + client.BaseAddress);
            // client.BaseAddress = client.BaseAddress;
        return  await getSinistri();
         

        }
    }
    // fetch dati da url dichiarata sopra
    public async Task<List<SinistriModel>> getSinistri()
    {

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


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