namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class Incarichi {
    public string idIncarico {get;set;}
    public string codiceAniaCompagnia {get;set;}
    public DateTime dataSinistro {get;set;}
    public DateTime dataDenuncia {get;set;}
    public DateTime dataIncarico {get;set;}
    public string numeroSinistro {get;set;}
    public string tipoSinistro {get;set;}
    public string tipoIncarico {get;set;}
    public string codicePerito {get;set;}
    public string nomePerito {get;set;}
    public string emailPerito {get;set;}
    public DateTime dataPerizia {get;set;}
    public DateTime dataRientro {get;set;}
    public DateTime dataChiusura {get;set;}
    public string assicuratoCognome {get;set;}
    public string assicuratoNome {get;set;}
    public string assicuratoVeicoloTarga {get;set;}
    public string controparteNomeCompagnia {get;set;}
    public string controparteVeicoloTarga {get;set;}

}