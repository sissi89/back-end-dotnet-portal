namespace WebApi.Models;

using WebApi.Entities;
/* "numsx": "0022991961670834486",
"data": "2022-11-24",
"numFile": 3,
"numCom": "?",
"stato": "Acquisito",
"isSollecita": true,
"isAnnulla": true,
"isRiapri": false,
"codComp": "920",
"nomeComp": "N.M.G. SRL" */
public class Detail {
    public string idInc {get;set;}
    public string numsx {get;set;}
    public string data {get;set;}
    public int numFile {get;set;}
    public string numCom {get;set;}
    public string stato {get;set;}
    public Boolean isSollecita {get;set;}
    public Boolean isRiapri {get;set;}
    public string codComp {get;set;}
    public string nomeComp {get;set;}
}