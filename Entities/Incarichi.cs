namespace WebApi.Entities;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
public class Incarichi {
 
    [Key]
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

/*
Win32Exception: Impossibile trovare il percorso di rete.
Unknown location

SqlException: A network-related or instance-specific error occurred while establishing
 a connection to SQL Server. The server was not found or was not accessible. 
 Verify that the instance name is correct and that SQL Server is configured to allow remote connections.
  (provider: Named Pipes Provider, 
error: 40 - Could not open a connection to SQL Server)*/