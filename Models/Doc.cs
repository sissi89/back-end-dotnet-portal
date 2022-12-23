/* export interface Doc{
"id": 8498496,
"nome": "autosoft.txt",
"tipo": "INCARICO",
"dim": 1750,
"daCom": true,
"idCom": null,
"nomeViewUrl": "<a href='portale-allegato.php?id=8498496' target='_blank'>autosoft.txt <i class='far fa-eye'></i></a>",
"downUrl": "<a href='portale-allegato.php?id=8498496&down'><i class='fas fa-download'></i></a>",
"sizeKB": "2KB",
"daComIcon": "<i class=\"far fa-building\"></i>",
"comUrl": "" */

namespace WebApi.Models;

using WebApi.Entities;

public class Doc {
  public string id {get;set;}
  public string nome {get;set;}
  public string tipo {get;set;}
  public int dim {get;set;}
  public Boolean daCom {get;set;}
  //"idCom": null,
  public  string  idCom {get;set;} // da far vedere è number ma se metto int e becca un null va in errore 
  public string nomeViewUrl {get;set;}
public string sizeKB {get;set;}
public string daComIcon {get;set;}
public string comUrl {get;set;}


}
