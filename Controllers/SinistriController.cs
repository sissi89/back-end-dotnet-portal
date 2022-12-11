namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

[ApiController]
[Route("[controller]")]

public class SinistriController : ControllerBase {
   
    private ISinistriService _sinistriService;

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

// dettaglio del servizio by id
[HttpGet ("sinistro/{id}")]
 public SinistriModel getId(string id){
    var sinistro = _sinistriService.GetSinistroID(id);
    return sinistro;
 }


   // get servizi con username = fiduciario
    [HttpGet("{username}")]
    public IActionResult  getByUsername(string username){
        var sinistri = _sinistriService.GetSinistriByFiduciario(username);
        if (username == null){
            return BadRequest(new { message = "Username non corretto" });
        }else{
                  return Ok(sinistri);
        }
            
  
    }
   

// dettagli pratica 
 [HttpGet("{username}/{id}")]

 public SinistriModel getDetailPratical(string username, string id){
    var sinistro = _sinistriService.GetPraticalDetail(username,id);
    return sinistro;
 }

}