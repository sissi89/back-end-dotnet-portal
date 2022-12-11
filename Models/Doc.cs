/* export interface Doc{
    name: string, 
    url:string,
    lastModified: string,
    lastModifiedDate: Date,
    webkitRelativePath: string, 
    size: number
} */

namespace WebApi.Models;

using WebApi.Entities;

public class Doc {
    public string name {get;set;}
    public string url {get;set;}
    public string lastModified {get;set;}

    public DateTime   lastModifiedDate {get;set;}

    public string  webkitRelativePath {get;set;}

    public int number;
}