namespace WebApi.Controllers;

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
    [HttpGet("sinistro/{id}")]
    public   SinistriModel getId(string id)
    {
      
    try {
        var sinistro = _sinistriService.GetSinistroID(id);
        return sinistro;
    }
    catch (IndexOutOfRangeException e)
    {
        System.Console.WriteLine(e);
        throw new ArgumentOutOfRangeException(
            "Parameter index is out of range.", e);
    }
    }


    // get servizi con username = fiduciario
    [HttpGet("{username}")]
    public async Task<IActionResult> getByUsername(string username)
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
    [HttpGet("{username}/{id}")]

    public SinistriModel getDetailPratical(string username, string id)
    {
        var sinistro = _sinistriService.GetPraticalDetail(username, id);
        return sinistro;
    }

}