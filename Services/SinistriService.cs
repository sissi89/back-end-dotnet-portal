
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
    IEnumerable<SinistriModel> GetSinistriByFiduciario(SinistroRequest username);
   

    SinistriModel GetSinistroID(SinistroRequest id);
    Task<List<SinistriModel>> Index();
    Task<SinistriModel> GetPraticalDetail2(SinistroRequest  body);
}
public class SinistriService : ISinistriService
{

    public List<SinistriModel> _sinistriNode = new List<SinistriModel> { };

    // parametri sorta di database dichiarato come un array list  array da sostituire
    private List<SinistriModel> _sinistri = new List<SinistriModel>{
        new SinistriModel{
        id ="1"   ,tipo =   "red"  , compa = "45",fiduciario="0001"  ,tipo_sinistro="1" ,
        data_incarico =  "2022-10-9 " ,
      nr_incarico  = "345679" ,
      prestazione_richiesta  =  "",
      assicurato  =  "Silvana Mazzaglia " ,
      targa_assicurato  = "AX918HG" ,
      controparte  = "Mario Rossi" ,
      targa_controparte  = "BG564HY" ,
      nr_int  ="23",
      data_ultimo  =" 2022-9-23 "

    },
    new SinistriModel{
         id  = "2",
         tipo  = "green" ,
         compa  ="456",
         fiduciario  = "0002",
         tipo_sinistro  ="2" ,
         data_incarico  = "2022-5-9" ,
         nr_sinistro = "123",
         nr_incarico  = "34567" ,
         prestazione_richiesta  = "" ,
         assicurato  = "Mario Bianchi" ,
         targa_assicurato  = "AX918YU" ,
         controparte  =  "Penelope Cruz" ,
         targa_controparte  = "BY564HY" ,
         nr_int  ="21",
         data_ultimo  = "2022-9-23" ,
    },
    new SinistriModel{
         id  =  "3" ,
         tipo  = "green" ,
         compa  ="654",
         fiduciario  = "0001" ,
         tipo_sinistro  = "3",
         data_incarico  = "2022-4-7" ,
         nr_sinistro = "234",
         nr_incarico  = "34567" ,
         prestazione_richiesta  =  "",
         assicurato  = "Mario Bianchi" ,
         targa_assicurato  = "AX918HG ",
         controparte  =  "Penelope Cruz ",
         targa_controparte  = "BG564HY" ,
         nr_int  ="21",
         data_ultimo  = "2022-9-23" ,
    }, new SinistriModel{
         id  =  "4",
         tipo  = "yellow" ,
         compa  ="476",
         fiduciario  ="0002" ,
         tipo_sinistro  = "2" ,
         data_incarico  = "2022-2-7" ,
         nr_sinistro  = "456" ,
         nr_incarico  = "4444 ",
         prestazione_richiesta  = "" ,
         assicurato  = "atzeni alessandro" ,
         targa_assicurato  = "AX918TG" ,
         controparte  =  "Penelope Cruz" ,
         targa_controparte  = "BG564OY" ,
         nr_int  ="21",
         data_ultimo  = "2022-9-23" ,
    },new SinistriModel{
         id  =  "5" ,
         tipo  = "yellow",
         compa  ="875",
         fiduciario  = "0001" ,
         tipo_sinistro  = "1",
         data_incarico  = "2022-1-7 ",
         nr_sinistro  = "45676" ,
         nr_incarico  = "345679" ,
         prestazione_richiesta  = "" ,
         assicurato  = "verdi alessandro" ,
         targa_assicurato  = "CG918TG" ,
         controparte  = "Rossi Mario" ,
         targa_controparte  = "BG564OY ",
         nr_int  ="21",
         data_ultimo  = "2022-9-23" ,
    },new SinistriModel{
         id  =  "6" ,
         tipo  = "red" ,
         compa  ="875",
         fiduciario  = "0002" ,
         tipo_sinistro  = "3" ,
         data_incarico  = "2022-4-7" ,
         nr_sinistro  = "45676" ,
         nr_incarico  = "345679" ,
         prestazione_richiesta  = "" ,
         assicurato  = "verdi alessandro" ,
         targa_assicurato  = "CG918TG" ,
         controparte  = "Rossi Mario" ,
         targa_controparte  =" BG564OY ",
         nr_int  ="21",
         data_ultimo  =" 2022-9-23 "
    }, new SinistriModel{
         id  =  "7" ,
         tipo  = "red" ,
         compa  ="875",
         fiduciario  = "0001" ,
         tipo_sinistro  = "3",
         data_incarico  = "2022-3-7 ",
         nr_sinistro  = "45676" ,
         nr_incarico  = "34567 ",
         prestazione_richiesta  =  "" ,
         assicurato  = "verdi alessandro" ,
         targa_assicurato  = "CG918TG" ,
         controparte  = "Rossi Mario" ,
         targa_controparte  = "BG564OY" ,
         nr_int  ="21",
         data_ultimo  = "2022-9-23 "
    },new SinistriModel{
         id  =  "8" ,
         tipo  = "yellow" ,
         compa  ="875",
         fiduciario  = "0002" ,
         tipo_sinistro  = "2",
         data_incarico  = "2022-9-7 ",
         nr_sinistro  = "45675" ,
         nr_incarico  = "347654" ,
         prestazione_richiesta  =  "" ,
         assicurato  = "Rossi Mario" ,
         targa_assicurato  = "BG564OY" ,
         controparte  = " verdi alessandro" ,
         targa_controparte  = " CG918TG" ,
         nr_int  ="21",
         data_ultimo  = "2022-9-23 "
    }};


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


    // sinistro by id 
    public SinistriModel GetSinistroID(SinistroRequest request)

    {
        // System.Console.WriteLine("sinistri by fiduciario"+request.id);

        SinistriModel sinistro = _sinistri.FirstOrDefault(x => x.id == request.id);

        System.Console.WriteLine("sinistro service" + sinistro);

        return sinistro;
    }

    // sinistri by username
    public IEnumerable<SinistriModel> GetSinistriByFiduciario(SinistroRequest username)
    {

        //List<int> termsList = new List<int>(); /7 array list
        //  List<SinistriModel> sinistribyUsername = new List<SinistriModel>();



        System.Console.WriteLine("sinistri by fiduciario tutti i sinistri");
        return _sinistri.FindAll(item => item.fiduciario == username.username);






    }

    // sinistro by username e id
    
    public async Task<SinistriModel> GetPraticalDetail2(SinistroRequest body)
    {

        System.Console.WriteLine("id: sono nell getprtica controller  ");
        // mi prendo tutti i sinistri 
        List<SinistriModel> sinistri = await Index();
       // System.Console.WriteLine("sinistri  " + sinistri + " " + sinistri.Count);
        // System.Console.WriteLine("sinistro service"+sinistro);
        List<SinistriModel> sinistriFiduciario =sinistri.FindAll(item => item.fiduciario == body.username);

        SinistriModel sinistro = sinistriFiduciario.FirstOrDefault(x => x.id == body.id);
        return sinistro;
        //return sinistro;


    }
        // all sinistri chiamata
    public async Task<List<SinistriModel>> Index()
    {
        string url = "http://localhost:3000/sinistri";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();
        string data = await response.Content.ReadAsStringAsync();
       // System.Console.WriteLine(data);

        Console.WriteLine("fatto");
        //   = JsonConvert.DeserializeObject<List<SinistriModel>>(data);
        // var sinistro =  JsonConvert.DeserializeObject<List<SinistriModel>>(data);
        var prova = JsonConvert.DeserializeObject<List<SinistriModel>>(data);
       // Console.WriteLine(prova + "  " + "prova");
        for (int i = 0; i < prova.Count; i++)
        {
            _sinistriNode.Add(prova[i]);
        }
        System.Console.WriteLine(_sinistriNode.Count);
        return _sinistriNode;
    }
}