namespace WebApi.Models;

using WebApi.Entities;

//Tutti i documenti per singolo incarico:
 // Produzione = "https://webapp.sogesa.net/portale/jarvis.php?do=incarico&idincarico="
public class DocumentiSingolaPratica{
    public int id {get;set;}
    public string nome {get;set;}
    public string tipo {get;set;}
    public int dim {get;set;}
    public Boolean daCom {get;set;}
    public string idCom {get;set;}

    public string nomeViewUrl {get;set;}
    public string downUrl {get;set;}
    public string sizeKB {get;set;}
    public string daComIcon {get;set;}
    public string comUrl {get;set;}


}

// tipo incarico lascia stare 

// completa le cose che hai fatto "idinc": "_SO2254361",
