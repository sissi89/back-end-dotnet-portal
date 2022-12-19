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
    // get tutti gli i servizi
     [HttpGet]

    public async Task<IActionResult> getIndex()
    {
       //  await Task.Delay(1000);
         return Ok(await _sinistriService.Index());
    } 
 
    // dettaglio del servizio 
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


    }

    // dettagli pratica 
    [HttpPost("sinistroFiduciario")]
    public async Task< SinistriModel> getDetailPratical(SinistroRequest body)
    {
      //   await Task.Delay(1000);

        var sinistro = await _sinistriService.GetPraticalDetail2(body);
        return sinistro;
    } 


    
    


}

