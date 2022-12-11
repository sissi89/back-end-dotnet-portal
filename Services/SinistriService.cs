
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


public interface ISinistriService{
    IEnumerable<SinistriModel> GetAll();
    IEnumerable<SinistriModel> GetSinistriByFiduciario(string username);
    SinistriModel GetPraticalDetail(string username, string id);

    SinistriModel GetSinistroID(string id);
    string getString(string dato);
}
public class SinistriService  : ISinistriService {
 

    // parametri sorta di database dichiarato come un array list 
    private List<SinistriModel> _sinistri =  new List<SinistriModel>{
        new SinistriModel{ 
        id ="1"   ,tipo =   "red"  , compa = 45,fiduciario="0001"  ,tipo_sinistro="1" ,
        data_incarico =  "2022-10-9 " ,
      nr_incarico  = 345679 ,
      prestazione_richiesta  =  "",
      assicurato  =  "Silvana Mazzaglia " ,
      targa_assicurato  = "AX918HG" ,
      controparte  = "Mario Rossi" ,
      targa_controparte  = "BG564HY" ,
      nr_int  =23,
      data_ultimo  =" 2022-9-23 "

    },
    new SinistriModel{
         id  = "2",
         tipo  = "green" ,
         compa  =456,
         fiduciario  = "0002",
         tipo_sinistro  ="2" ,
         data_incarico  = "2022-10-9" ,
         nr_sinistro = 123,
         nr_incarico  = 34567 ,
         prestazione_richiesta  = "" ,
         assicurato  = "Mario Bianchi" ,
         targa_assicurato  = "AX918YU" ,
         controparte  =  "Penelope Cruz" ,
         targa_controparte  = "BY564HY" ,
         nr_int  =21,
         data_ultimo  = "2022-9-23" ,
    },
    new SinistriModel{
         id  =  "3" ,
         tipo  = "green" ,
         compa  =654,
         fiduciario  = "0001" ,
         tipo_sinistro  = "3",
         data_incarico  = "2022-9-7" ,
         nr_sinistro = 234,
         nr_incarico  = 34567 ,
         prestazione_richiesta  =  "",
         assicurato  = "Mario Bianchi" ,
         targa_assicurato  = "AX918HG ",
         controparte  =  "Penelope Cruz ",
         targa_controparte  = "BG564HY" ,
         nr_int  =21,
         data_ultimo  = "2022-9-23" ,
    }, new SinistriModel{
         id  =  "4",
         tipo  = "yellow" ,
         compa  =476,
         fiduciario  ="0002" ,
         tipo_sinistro  = "2" ,
         data_incarico  = "2022-9-7" ,
         nr_sinistro  = 456 ,
         nr_incarico  = 4444 ,
         prestazione_richiesta  = "" ,
         assicurato  = "atzeni alessandro" ,
         targa_assicurato  = "AX918TG" ,
         controparte  =  "Penelope Cruz" ,
         targa_controparte  = "BG564OY" ,
         nr_int  =21,
         data_ultimo  = "2022-9-23" ,
    },new SinistriModel{
         id  =  "5" ,
         tipo  = "red ",
         compa  =875,
         fiduciario  = "0001" ,
         tipo_sinistro  = "1",
         data_incarico  = "2022-7-7 ",
         nr_sinistro  = 45676 ,
         nr_incarico  = 345679 ,
         prestazione_richiesta  = "" ,
         assicurato  = "verdi alessandro" ,
         targa_assicurato  = "CG918TG" ,
         controparte  = "Rossi Mario" ,
         targa_controparte  = "BG564OY ",
         nr_int  =21,
         data_ultimo  = "2022-9-23" ,
    },new SinistriModel{
         id  =  "6" ,
         tipo  = "red" ,
         compa  =875,
         fiduciario  = "0002" ,
         tipo_sinistro  = "3" ,
         data_incarico  = "2022-7-7" ,
         nr_sinistro  = 45676 ,
         nr_incarico  = 345679 ,
         prestazione_richiesta  = "" ,
         assicurato  = "verdi alessandro" ,
         targa_assicurato  = "CG918TG" ,
         controparte  = "Rossi Mario" ,
         targa_controparte  =" BG564OY ",
         nr_int  =21,
         data_ultimo  =" 2022-9-23 "
    }, new SinistriModel{
         id  =  "7" ,
         tipo  = "red" ,
         compa  =875,
         fiduciario  = "0001" ,
         tipo_sinistro  = "3 ",
         data_incarico  = "2022-7-7 ",
         nr_sinistro  = 45676 ,
         nr_incarico  = 34567 ,
         prestazione_richiesta  =  "" ,
         assicurato  = "verdi alessandro" ,
         targa_assicurato  = "CG918TG" ,
         controparte  = "Rossi Mario" ,
         targa_controparte  = "BG564OY" ,
         nr_int  =21,
         data_ultimo  = "2022-9-23 "
    }};

 private readonly AppSettings _appSettings;

    public SinistriService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
public IEnumerable<SinistriModel> GetAll()
    {
        // console.log
        System.Console.Write("service");
        return _sinistri;
    }


// sinistro by id 
 public SinistriModel GetSinistroID(string id)
    {
          return _sinistri.FirstOrDefault(x => x.id == id);
    }

// sinistri by username
public IEnumerable<SinistriModel> GetSinistriByFiduciario(string username){
    //List<int> termsList = new List<int>(); /7 array list
List<SinistriModel> sinistribyUsername= new List<SinistriModel> ();

for (int i = 0 ; i<_sinistri.Count ; i++){
    if(_sinistri[i].fiduciario == username){
     
       sinistribyUsername.Add(_sinistri[i]);
    }
}

return sinistribyUsername;


}
// prova api
public string getString(string dato){
return dato;
}
    // sinistro by username e id

    
    public   SinistriModel GetPraticalDetail(string username, string id)
    {
         List<SinistriModel>  sinistri =  (List<SinistriModel>)GetSinistriByFiduciario(username);
         System.Console.WriteLine("sinistri"+ sinistri);
       /*       for(int i = 0; i < sinistri.Count ; i++){
        if(sinistri[i].id == id){
          return sinistri[i];
        } */
         // find di typescript
        return sinistri.FirstOrDefault(x => x.id == id);
      
     
    }

   
}