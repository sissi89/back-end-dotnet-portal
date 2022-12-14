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

    public async Task<IActionResult> getIndex()
    {
       //  await Task.Delay(1000);
         return Ok(await _sinistriService.Index());
    } 
 
    // dettaglio del servizio 
    [HttpPost("sinistro")]
    public  async Task<IActionResult>  getId([FromBody] SinistroRequest id)
    {
         await Task.Delay(1000);
       // System.Console.WriteLine("prova post"+id);
        var sinistro = _sinistriService.GetSinistroID(id);
       // System.Console.WriteLine("sinistro:"+sinistro);
        return Ok(sinistro);
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

 [HttpGet("prove/{id}")]

 public async Task<SinistriModel> getPraticaDetail2( SinistroRequest id){
     
System.Console.WriteLine("id: sono nell getprtica controller  ");
             var sinistro = await    _sinistriService.GetPraticalDetail2(id);
            return  sinistro;
        
 }
    // dettagli pratica 
    [HttpPost("sinistroFiduciario")]

   

    public async Task< SinistriModel> getDetailPratical(SinistroRequest body)
    {
         await Task.Delay(1000);
        var sinistro = _sinistriService.GetPraticalDetail(body);
        return sinistro;
    } 

/* [HttpGet("prova")]
   public async Task<IActionResult> getIndex(){
    //GetTodosWithJsonExtension
  return Ok(await _sinistriService.Index());

    
 }
 */


    
    


}

