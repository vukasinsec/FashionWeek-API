using Microsoft.AspNetCore.Mvc;
using FashionWeekLibrary;
using FashionWeekLibrary.DTOs;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("[controller]")]
public class ModnaRevijaController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiModneRevije")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetModneRevije()
    {
        (bool isError, var revije, ErrorMessage? error) = DataProvider.VratiSveModneRevije();

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(revije);
    }
    
    [HttpPost]
    [Route("DodajModnuReviju")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddModnaRevija([FromBody] ModnaRevijaView m)
    {
        var data = await DataProvider.DodajModnuRevijuAsync(m);
        
        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return StatusCode(201, $"Uspešno dodata modna revija. Naziv: {m.Naziv}");
    }

    [HttpPost("DodajModnuRevijuSaOrganizatorom/{idOrganizatora}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DodajModnuRevijuSaOrganizatorom([FromBody] ModnaRevijaView m, int idOrganizatora)
    {
        var (isError, naziv, error) = await DataProvider.SacuvajModnuReviju(m, idOrganizatora);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }



        return StatusCode(201, $"Upisana revija, sa nazivom: {naziv}");
    }

    [HttpPut]
    [Route("PromeniModnuReviju")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ChangeProdavnica([FromBody] ModnaRevijaView p)
    {
        (bool isError, var revija, ErrorMessage? error) = await DataProvider.AzurirajModnuRevijuAsync(p);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (revija == null)
        {
            return BadRequest("Prodavnica nije validna.");
        }

        return Ok($"Uspešno ažurirana revija. Naziv: {revija.Naziv}");
    }
    
    [HttpDelete]
    [Route("ObrisiModnuReviju/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObrisiModnuReviju(int id)
    {
        var data = await DataProvider.ObrisiModnuRevijuAsync(id);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        //return StatusCode(204, $"Uspešno obrisana revija. ID: {id}");
        
        return Ok();
    }

    [HttpGet]
    [Route("PreuzmiSpecijalneGosteNaModnojReviji/{IDModneRevije}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetSpecijalneGoste(int IDModneRevije)
    {
        (bool isError, var gosti, var error) = DataProvider.SpecijalniGostiNaModnojReviji(IDModneRevije);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(gosti);
    }

    

    [HttpGet]
    [Route("PreuzmiRevijeManekena/{mbr}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetRevijeManekena(string mbr)
    {
        (bool isError, var revije, var error) = DataProvider.VratiRevijeManekena(mbr);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(revije);
    }

    

   


}
