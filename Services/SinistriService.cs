
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
    Task<SinistriFiduciario[]> getSinistribyFiduciario(string fiduciario);
    Task<List<SinistriModel>> getFakeSinistri();
    Task<SinistriModel[]> getSinistriDate(string start, string end);
    Task<Detail> getDettail(string IdInc);
    Task<Doc[]> getDocumentsIncarico(string idInc);
    Task<SinistriModel[]> getSinistriByFiduciarioFixed();

    Task<byte[]> getDocument(string url);
   
    Task<SinistriModel[]> getSinistriByFiduciario(string start, string end, string perito);
    // api da database
    IEnumerable<Incarichi> GetAll();
    IEnumerable<Incarichi> getAll();
}
public class SinistriService : ISinistriService
{
    private DataContext _context;

    public List<SinistriModel> _sinistriNode = new List<SinistriModel> { };
    // dichiaro client che mi serve per fare la fetch dei dati
    static HttpClient client = new HttpClient();
    // parametri sorta di database dichiarato come un array list  array da sostituire
    // string url = "http://localhost:3000/sinistri";


    private List<SinistriModel> sinistriFetch = new List<SinistriModel> { };
    private readonly AppSettings _appSettings;
   // private readonly IMapper _mapper;
    public SinistriService(IOptions<AppSettings> appSettings, DataContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;
    }

    // get dettaglio singola pratica
    public async Task<Detail> getDettail(string IdInc)
    {

        string url = "http://webapp.sogesa.net/portale/jarvis.php?do=incarico&idincarico=" + IdInc;
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
        string url = "http://webapp.sogesa.net/portale/jarvis-incarichi.php?id_filtro=0&start=" + start + "&end=" + end;
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
    // get sinistri by fiduciario non funziona
    public async Task<SinistriModel[]> getSinistriByFiduciario(string start, string end, string perito)
    {
        Console.WriteLine("sono nel controller");
        string url = "http://webapp.sogesa.net/portale/jarvis-incarichi.php?id_filtro=0&start=" + start + "&end=" + end;
        Console.WriteLine("perito prima", perito);

        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            var sinistri = JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
            System.Console.WriteLine(sinistri);
            //item => item.fiduciario == body.username
            SinistriModel[] sinistriByPerito = { };
            for (int i = 0; i < sinistri.Length; i++)
            {
                if (sinistri[i].codPer == perito)
                {
                    Console.WriteLine("perito", sinistri[i].codPer);
                    sinistriByPerito.Append(sinistri[i]);
                }
                else
                {

                }
            }
            Console.WriteLine("sinistri", sinistri);
            return sinistri;

        }
        else
        {

            var sinistri = JsonConvert.DeserializeObject<SinistriModel[]>(await call(url));
            //item => item.fiduciario == body.username
            SinistriModel[] sinistriByPerito = { };
            for (int i = 0; i < sinistri.Length; i++)
            {
                if (sinistri[i].nomePer == perito)
                {
                    sinistriByPerito.Append(sinistri[i]);
                }
                else
                {
                    return null;
                }
            }
            return sinistri;

        }
    }
    // get dowloand documento del id incarico

    public async Task<byte[]> getDocument(string idInc)
    {
        string url = "http://webapp.sogesa.net/portale/jarvis-allegato.php?id=" + idInc;
        var result = await client.GetAsync(url);
        return result.IsSuccessStatusCode ? await result.Content.ReadAsByteArrayAsync() : null;
    }
    // get documenti di un incarico 
    public async Task<Doc[]> getDocumentsIncarico(string idInc)
    {
        //  System.Console.WriteLine("sono nel service documents");
        string url = "http://webapp.sogesa.net/portale/jarvis.php?do=allegati&idincarico=" + idInc;
        var json = JsonConvert.DeserializeObject<Doc[]>(await call(url));

        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(url);
            //  Console.WriteLine("'json if '");
            if (json != null)
            {
                //   Console.WriteLine("il json non è null");
                return json;
            }
            else
            {
                Console.WriteLine("il json  è null");
                return json;
            }


        }
        else
        {
            //    Console.WriteLine("'json else '");
            if (json != null)
            {
                Console.WriteLine("il json non è null");
                return json;
            }
            else
            {
                Console.WriteLine("il json  è null");
                return json;
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
    // incarichi per sx 
    public async Task<SinistriFiduciario[]> getSinistribyFiduciario(string numSx)
    {
        System.Console.WriteLine("sono nel service");
        string url = "http://webapp.sogesa.net/portale/jarvis.php?do=incarichi&numsx=" + numSx;
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

    // Incarichi per Sx fisso
    public async Task<SinistriModel[]> getSinistriByFiduciarioFixed()
    {
        string url = "http://webapp.sogesa.net/portale/jarvis.php?do=incarichi&numsx=0044587201670335665";
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
            //   System.Console.WriteLine("response"+ response);
            response.EnsureSuccessStatusCode();
          
            // mi salvo tutti i dati in una stringa e return in modo tale che posso variare il tipo ad ogni metodo
            string stringa = await response.Content.ReadAsStringAsync();
            // System.Console.WriteLine("'prova'"+stringa);
            return stringa;
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
    /*------- api from Database ------ */
    public IEnumerable<Incarichi> GetAll()
    {
        return _context.Incarichi;
    }
   public IEnumerable<Incarichi> getAll(){
    FormattableString sql = $"SELECT * FROM [Incarichi_Table]";
    System.Console.WriteLine("sql",sql);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
    var incarichi =  _context.Incarichi.FromSql(sql);
  incarichi.Count();
    return  incarichi;
   }
   
    





}