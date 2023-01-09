namespace WebApi.Controllers;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

[ApiController]
[Route("[controller]")]

public class SinistriController : ControllerBase
{

    private ISinistriService _sinistriService;


    public SinistriController(ISinistriService sinistriService)
    {
        _sinistriService = sinistriService;

    }
    /*   // get tutti gli i servizi
       [HttpGet]

      public async Task<IActionResult> getIndex()
      {
         //  await Task.Delay(1000);
           return Ok(await _sinistriService.Index());
      } */

    /* // dettaglio del servizio 
     [HttpPost("sinistro")]
     public async Task<SinistriModel> getId([FromBody] SinistroRequest id)
     {
        //  await Task.Delay(100);
        // System.Console.WriteLine("prova post"+id);
         var sinistro = await  _sinistriService.GetSinistroID(id);
        // System.Console.WriteLine("sinistro:"+sinistro);
         return sinistro;
     }


     // get servizi con username = fiduciario
     [HttpPost("fiduciario")]
     public async Task<List<SinistriModel>> getByUsername([FromBody] SinistroRequest username)
     {

         var sinistri = await _sinistriService.GetSinistriByFiduciario(username);
         return sinistri;


     } */

    // dettagli pratica 
    /*   [HttpPost("sinistroFiduciario")]
      public async Task< SinistriModel> getDetailPratical(SinistroRequest body)
      {
        //   await Task.Delay(1000);

          var sinistro = await _sinistriService.GetPraticalDetail2(body);
          return sinistro;
      }  */

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> getAllSinistriWithDate([FromBody] SinistroRequest body){
        if(body.start != null && body.end != null){
            var incarichi = await _sinistriService.getSinistriDate(body.start, body.end);
            if(incarichi.Length >0){
                return Ok(incarichi);
            }else{
                return StatusCode(404,"Incarichi non trovati");
            }
        }else{
            return BadRequest();
        }
        
    }
    [HttpPost("incarichi")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> getDettail([FromBody] SinistroRequest body){
        if(body.idInc != null){
            var incarico = await _sinistriService.getDettail(body.idInc);
            return Ok(incarico);
        }else{
            return StatusCode(404, "IdInc non trovato");
           // return BadRequest();
        }

    }


    
    //  Tutti i documenti per singolo incarico:

    [HttpPost("incarichi/documents")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> getDocumentsIncarico([FromBody] SinistroRequest body)
    {
        if(body.idInc != null){
            var documents = await _sinistriService.getDocumentsIncarico(body.idInc);
          /*   if(documents != null )
            return Ok(documents); */
          if(documents != null){
            return Ok(documents);
          }else{
            return StatusCode(404,"idInc non trovato");
          }
        }else{
            return BadRequest();
        }
    }
    

    // dowloand documento
     [HttpGet("documents/singolo/{idInc}")]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult getSingleDocument(string idInc)
    {
         var result = _sinistriService.getDocument(idInc);
        return File(result.Result, "application/pdf");

    } 
    // da provare
  
    // sinistri by fiduciario non funziona
    [HttpGet("incarichi/fiduciario/{start}/{end}/{perito}")]
    public IActionResult getSinistriPerito(string start, string end, string perito)
    {
        var incarico = _sinistriService.getSinistriByFiduciario(start, end, perito);
        System.Console.WriteLine("'console'", incarico);
        return Ok(incarico);
    }
/*----- api from database ---- */
// allIncarichi 

[HttpGet("incarichi2")]
public IActionResult getIncarichi(){
    var incarichi =_sinistriService.getAll();
    return Ok(incarichi);
}

}

