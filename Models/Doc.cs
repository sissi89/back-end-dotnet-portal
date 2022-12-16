/* export interface Doc{
  {
                "id":1,
                "name":"Example",
                "document":"https://res.cloudinary.com/demo/image/upload/example_pdf"
                
            },{
                "id":2,
                "name":"Sample",
                "document":"https://www.africau.edu/images/default/sample.pdf"
            }
} */

namespace WebApi.Models;

using WebApi.Entities;

public class Doc {
  int id {get; set;}
  string name {get;set;}
DetailDocs documento {get;set;}


}
