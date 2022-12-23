
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
    /*   IEnumerable<SinistriModel> GetAll();
      //public Task<List<SinistriModel>> GetAll();
      //  IEnumerable<SinistriModel> GetSinistriByFiduciario(SinistroRequest username);
      Task<List<SinistriModel>> GetSinistriByFiduciario(SinistroRequest username);

      Task<SinistriModel> GetSinistroID(SinistroRequest request);
      Task<List<SinistriModel>> Index();
      Task<SinistriModel> GetPraticalDetail2(SinistroRequest body); */
    // get all sinistri da data fissa

    // 
    Task<SinistriModel[]> getSinistriByDateFixed();
     Task<SinistriFiduciario[]> getSinistribyFiduciario(string fiduciario); 
    Task<List<SinistriModel>> getFakeSinistri();
    Task<SinistriModel[]> getSinistriDate(string start, string end);
     Task<Detail>getDettail(string IdInc);
Task<Doc[]> getDocumentsIncarico(string idInc);
    Task<SinistriModel[]> getSinistriByFiduciarioFixed();

Task<byte[]> getDocument(string url);
Task<SinistriModel[]> getSinistriByFiduciario(string start, string end, string perito);
}
public class SinistriService : ISinistriService
{

    public List<SinistriModel> _sinistriNode = new List<SinistriModel> { };
    // dichiaro client che mi serve per fare la fetch dei dati
    static HttpClient client = new HttpClient();
    // parametri sorta di database dichiarato come un array list  array da sostituire
    // string url = "http://localhost:3000/sinistri";


    private List<SinistriModel> sinistriFetch = new List<SinistriModel> { };
    private readonly AppSettings _appSettings;

    public SinistriService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
    // tutti i sinistri con ruolo operatore
    /* public IEnumerable<SinistriModel> GetAll()
    //public  async Task<List<SinistriModel>> GetAll()
    {

        // console.log

        System.Console.WriteLine(_sinistriNode.Count);
        System.Console.WriteLine(_sinistriNode + " " + "sinistri node ");
        return _sinistriNode;
    } */


    // sinistro by id   [HttpPost("sinistro")]
    /*  public async Task<SinistriModel> GetSinistroID(SinistroRequest request)

     {
         System.Console.WriteLine("sinistri by fiduciario" + request.id);
         request.username = null;
         List<SinistriModel> sinistri = await Index();
         System.Console.WriteLine("i sinistri sono " + sinistri.Count);
         SinistriModel sinistro = sinistri.FirstOrDefault(x => x.id == request.id);

         System.Console.WriteLine("sinistro service" + sinistro.ToString());

         return sinistro;

     } */

    // sinistri by username     [HttpPost("fiduciario")]
    /*  public async Task<List<SinistriModel>> GetSinistriByFiduciario(SinistroRequest username)
     {

         //List<int> termsList = new List<int>(); /7 array list
         //   List<SinistriModel> sinistribyUsername = new List<SinistriModel>();
         // essendo che la classe Sinistro Request accetta 2 valori metto il valore che non serve a null
         username.id = null;

         List<SinistriModel> sinistri = await Index();
         System.Console.WriteLine("sinistri by fiduciario tutti i sinistri");
         return sinistri.FindAll(item => item.fiduciario == username.username);






     } */

    // sinistro by username e id  [HttpPost("sinistroFiduciario")]

    /*  public async Task<SinistriModel> GetPraticalDetail2(SinistroRequest body)
     {

         System.Console.WriteLine("id: sono nell get pratica controller  ");
         // mi prendo tutti i sinistri 
         List<SinistriModel> sinistri = await Index();
         System.Console.WriteLine("sinistri  " + sinistri + " " + sinistri.Count);
         // System.Console.WriteLine("sinistro service"+sinistro);
         List<SinistriModel> sinistriFiduciario = sinistri.FindAll(item => item.fiduciario == body.username);

         SinistriModel sinistro = sinistriFiduciario.FirstOrDefault(x => x.id == body.id);
         return sinistro;
         //return sinistro;


     } */
    // all sinistri chiamata 

    /*   public async Task<List<SinistriModel>> Index()
     //// client.BaseAddress va dichairato solo una volta 
     {
         if (client.BaseAddress == null)
         {
             System.Console.WriteLine("sono nell if " + client.BaseAddress);
             client.BaseAddress = new Uri(this.url);
            return  await getSinistri();


         }
         else
         {
             System.Console.WriteLine("sono nell else " + client.BaseAddress);
             // client.BaseAddress = client.BaseAddress;
         return  await getSinistri();


         }
     }  */
    // fetch dati da url dichiarata sopra
    /*  public async Task<List<SinistriModel>> getSinistri()
     {

         client.DefaultRequestHeaders.Accept.Clear();
         // gli dico che deve accettare file json 
         client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json")
         );
         try
         {
             //  System.Console.WriteLine("sono nel try catch");

             var response = await client.GetAsync(this.url);
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


     } */
     // get dettaglio singola pratica
     public async Task<Detail>getDettail(string IdInc){

        string url ="https://webapp.sogesa.net/portale/jarvis.php?do=incarico&idincarico="+IdInc;
        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            return JsonConvert.DeserializeObject<Detail>(await call(url));
        }
        else
        {
            return JsonConvert.DeserializeObject<Detail>(await call(url));
        }
     }

    // get sinistri by data optionale Tutti i SX ( per data ):
    public async Task<SinistriModel[]> getSinistriDate(string start, string end)
    {
        // https://webapp.sogesa.net/portale/jarvis-incarichi.php?id_filtro=0&start=2022-12-20&end=2022-12-21
        string url = "https://webapp.sogesa.net/portale/jarvis-incarichi.php?id_filtro=0&start=" + start + "&end=" + end;
        // client.BaseAddress = new Uri(url);
        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            return JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
        }
        else
        {
            return JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
        }


    }
    // ge sinistri by fiduciario 
    public async Task<SinistriModel[]> getSinistriByFiduciario(string start, string end, string perito){
        string url = "https://webapp.sogesa.net/portale/jarvis-incarichi.php?id_filtro=0&start=" + start + "&end=" + end;
         if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            var sinistri =  JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
            //item => item.fiduciario == body.username
            SinistriModel[] sinistriByPerito = {}; 
        for(int i =0;i < sinistri.Length; i++){
            if(sinistri[i].nomePer == perito){
                sinistriByPerito.Append(sinistri[i]);
            }else{
                return null;
            }
        }
           return sinistriByPerito;

        }
        else
       {
           
            var sinistri =  JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
            //item => item.fiduciario == body.username
            SinistriModel[] sinistriByPerito = {}; 
        for(int i =0;i < sinistri.Length; i++){
            if(sinistri[i].nomePer == perito){
                sinistriByPerito.Append(sinistri[i]);
            }else{
                return null;
            }
        }
           return sinistriByPerito;

        }
    }
    // get dowloand documento del id incarico

    public async Task<byte[]> getDocument(string url){
        var result = await client.GetAsync(url);
        return result.IsSuccessStatusCode ? await result.Content.ReadAsByteArrayAsync(): null;
    }
    // get documenti di un incarico 
    public async Task<Doc[]> getDocumentsIncarico(string idInc){
      //  System.Console.WriteLine("sono nel service documents");
        string url ="http://webapp.sogesa.net/portale/jarvis.php?do=allegati&idincarico="+idInc;
        var json = JsonConvert.DeserializeObject<Doc[]>(await call(url));
        
        if(client.BaseAddress == null){
            client.BaseAddress = new Uri(url);
          //  Console.WriteLine("'json if '");
          if(json != null){
            Console.WriteLine("il json non è null");
                return json;
          }else{
               Console.WriteLine("il json  è null");
                return json ;
          }
           
            
        }else {
          //    Console.WriteLine("'json else '");
          if(json != null){
            Console.WriteLine("il json non è null");
                return json;
          }else{
               Console.WriteLine("il json  è null");
                return json ;
          }
          
          
        }
    }

    
    // get fake sinistri
    public async Task<List<SinistriModel>> getFakeSinistri()
    {
        string url = "http://localhost:3000/sinistri";
        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            return JsonConvert.DeserializeObject<List<SinistriModel>>(await call(url));
        }
        else
        {
            return JsonConvert.DeserializeObject<List<SinistriModel>>(await call(url));
        }
    }
    // incarichi per sx fisso 
    public async Task<SinistriFiduciario[]> getSinistribyFiduciario(string fiduciario)
    {
        System.Console.WriteLine("sono nel service");
        string url = "https://webapp.sogesa.net/portale/jarvis.php?do=incarichi&numsx=" + fiduciario;
        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            var tmp = await call(url);
            //Console.WriteLine(tmp);
            return JsonConvert.DeserializeObject<SinistriFiduciario[]>(await call(url));
        }
        else
        {
            return JsonConvert.DeserializeObject<SinistriFiduciario[]>(await call(url));
        }
    }
    // get sinistri per fiduciario nomePer fixed con un fiduciario
    // Incarichi per Sx fisso
    public async Task<SinistriModel[]> getSinistriByFiduciarioFixed()
    {
        string url = "https://webapp.sogesa.net/portale/jarvis.php?do=incarichi&numsx=0044587201670335665";
        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            return JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
        }
        else
        {
            return JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
        }
    }
    // get sinistri by data fixed in url per prova Tutti i SX ( per data ):
    public async Task<SinistriModel[]> getSinistriByDateFixed()
    {
        string url = "https://webapp.sogesa.net/portale/jarvis-incarichi.php?id_filtro=0&start=2022-12-19&end=2022-12-20";

        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            //return await call(url);
            return JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
        }
        else
        {
            return JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
        }
    }
    // simulare una select come fare delle select con database

    public async Task<string> call(string url)
    {
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
            // mi salvo tutti i dati in una stringa e return in modo tale che posso variare il tipo ad ogni metodo
          
           // System.Console.WriteLine("'prova'"+prova);
            return await response.Content.ReadAsStringAsync();
            // converto in json
            //System.Console.WriteLine("data: "+data);
            //var json = JsonConvert.DeserializeObject<SinistriModel[]>(data.);
            //return json;
        }
        catch (Exception e)
        {

           System.Console.WriteLine("message di errore " + e);
            return null;

        }




    }






}