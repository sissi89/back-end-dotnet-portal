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
    private object message;

    public SinistriController(ISinistriService sinistriService)
    {
        _sinistriService = sinistriService;

    }
    // get tutti gli i servizi
     [HttpGet]

    public IActionResult GetAll()
    {
        var sinistri = _sinistriService.GetAll();
        return Ok(sinistri);
    }
 
    // dettaglio del servizio 
    [HttpPost("sinistro")]
    public   SinistriModel getId([FromBody] SinistroRequest id)
    {
       // System.Console.WriteLine("prova post"+id);
        var sinistro = _sinistriService.GetSinistroID(id);
       // System.Console.WriteLine("sinistro:"+sinistro);
        return sinistro;
    }


    // get servizi con username = fiduciario
    [HttpPost("fiduciario")]
    public async Task<IActionResult> getByUsername([FromBody] SinistroRequest username)
    {
         await Task.Delay(1000);

           if (username == null)
        {
            return BadRequest(new { message = "Username non corretto" });
        }
        else
        {
             var sinistri =   _sinistriService.GetSinistriByFiduciario(username);
            return  Ok(sinistri);
        }


    }


    // dettagli pratica 
    [HttpPost("sinistroFiduciario")]

    public SinistriModel getDetailPratical([FromBody] SinistroRequest body)

    {
        System.Console.WriteLine("sinistro fiducairio   "+ body);
        var sinistro = _sinistriService.GetPraticalDetail(body);
        return sinistro;
    } 

[HttpGet("prova")]
   public async Task<IActionResult> getIndex(){
    //GetTodosWithJsonExtension
  return Ok(await _sinistriService.Index());

    
 }


    
    


}

