namespace WebApi.Models;

using WebApi.Entities;

//Tutti i SX ( per data ):
// https://webapp.sogesa.net/portale/jarvis-incarichi.php?id_filtro=0&start=2022-12-20&end=2022-12-21


public class SinistriModel{
    //    public int Id { get; set; }
    public string numSx {get;set;}
    public string idInc {get;set;}
    public string dtsx {get;set;}
    public string dtInc {get;set;}
    public string codPer {get;set;}
    public string nomePer {get;set;}

    public string emailPer {get;set;}

    public string dtPer {get;set;}

    public string dtChiusura {get;set;}
    public string dtRientro {get;set;}
    public Boolean isCom {get;set;}
    public int numComLeggere {get;set;}
    public string isComText {get;set;}
    


  


}