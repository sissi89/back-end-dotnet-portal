namespace WebApi.Models;

using WebApi.Entities;

/* <tr class="tr-1"> 
                     <th class="th-1">Tipo di Urgenza</th>
                     <th class="th-1">Compagnia</th>
                     <th  *ngIf=" getRole() === 1" class="th-1">Fiduciario</th>
                     <th class="th-1"> Tipo sinistro</th>
                     <th class="th-1">Dt.incarico</th>
                     <th class="th-1"> Nr. Sinistro</th>
                     <th class="th-1"> Nr. Incarico</th>
                     <th class="th-1"> Prestazione </th>
                     <th class="th-1"> Assicurato</th>
                     <th class="th-1"> Controparte</th> */
public class SinistriModel{
    //    public int Id { get; set; }

    public string id {get;set;}
    public string tipo {get;set;}
    public int compa {get;set;}

    public string fiduciario {get;set;}

    public string tipo_sinistro {get;set;}

    public string data_incarico {get;set;}
    public int nr_incarico {get;set;}
public int nr_sinistro {get;set;}
    public string prestazione_richiesta {get;set;}

    public string assicurato {get;set;}

    public string targa_assicurato {get;set;}

    public string controparte {get;set;}

    public string targa_controparte {get;set;}

     public int nr_int {get;set;}

     public string data_ultimo {get;set;}

     public Doc[] document {get;set;}



}