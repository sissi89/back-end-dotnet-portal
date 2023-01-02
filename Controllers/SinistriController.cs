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

    [HttpGet("incarichiPer/{sinistro}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[]
    //getSinistribyFiduciario
    public async Task<IActionResult> getIncarichi(string perito)
    {
        var incarichi = await _sinistriService.getSinistribyFiduciario(perito);


        return Ok(incarichi);
    }





    //getSinistriDate(string start , string end) // 2 stringe
    [HttpGet("{dataStart}/{dataEnd}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> getAllSinistriWithDate(string dataStart, string dataEnd)
    {
        var incarichi = await _sinistriService.getSinistriDate(dataStart, dataEnd);
        return Ok(incarichi);
    }



    // get dettaglio singola pratica
    [HttpGet("incarichi/{idInc}")] // idInc
    [ProducesResponseType(typeof(SinistriModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> getDettail(string idInc)
    {
        if (idInc != null)
        {
            var incarico = await _sinistriService.getDettail(idInc);
            return Ok(incarico);
        }
        else
        {
          // var result = new ObjectResult(new { error = "IdInc non inserito" });
           // return StatusCode(404, "IdInc non trovato");
            return BadRequest();
        }




    }
    //  Tutti i documenti per singolo incarico:
    [HttpGet("incarichi/documents/{idInc}")] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> getDocumentsIncarico(string idInc)
    {
        var documents = await _sinistriService.getDocumentsIncarico(idInc);
        return Ok(documents);
    }
    // dowloand documento
    [HttpGet("documents/singolo/{idInc}")]
    public IActionResult getSingleDocument(string idInc)
    {
         var result = _sinistriService.getDocument(idInc);
        return File(result.Result, "application/pdf");

    }
    // sinistri by fiduciario
    [HttpGet("incarichi/fiduciario/{start}/{end}/{perito}")]
    public IActionResult getSinistriPerito(string start, string end, string perito)
    {
        var incarico = _sinistriService.getSinistriByFiduciario(start, end, perito);
        System.Console.WriteLine("'console'", incarico);
        return Ok(incarico);
    }



}

