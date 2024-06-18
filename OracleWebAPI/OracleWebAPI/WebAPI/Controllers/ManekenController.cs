using Microsoft.AspNetCore.Mvc;
using FashionWeekLibrary;
using FashionWeekLibrary.DTOs;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("[controller]")]
public class ManekenController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiManekene")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetManekeni()
    {
        (bool isError, var manekeni, ErrorMessage? error) = DataProvider.VratiSveManekene();

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(manekeni);
    }

    [HttpPost]
    [Route("DodajManekena")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddManekena([FromBody] ManekenView m)
    {
        var data = await DataProvider.DodajManekena(m);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return StatusCode(201, $"Uspešno dodat maneken. Ime: {m.Ime}");
    }

    [HttpPut]
    [Route("IzmeniManekena")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateManekena([FromBody] ManekenView ma)
    {
        (bool isError, var maneken, ErrorMessage? error) = await DataProvider.AzurirajManekena(ma);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (maneken == null)
        {
            return BadRequest("Maneken nije validan.");
        }

        return Ok($"Uspešno ažuriran maneken. ime: {maneken.Ime}");
    }

    [HttpDelete]
    [Route("ObrisiManekena/{mbr}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteManekena(string mbr)
    {
        var data = await DataProvider.ObrisiManekena(mbr);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        //return StatusCode(204, $"Uspešno obrisan maneken. mbr: {mbr}");
        return Ok();
    }

    [HttpGet]
    [Route("PreuzmiManekeneSaModneRevije/{IDModneRevije}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetManekeniModneRevije(int IDModneRevije)
    {
        (bool isError, var manekeni, var error) = DataProvider.VratiSveManekeneModneRevije(IDModneRevije);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(manekeni);
    }

    [HttpGet("PreuzmiManekeneModneAgencije/{pib}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetManekeneModneAgencije(string pib)
    {
        var (isError, manekeni, error) = await DataProvider.VratiSveManekeneModneAgencije(pib);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(manekeni);
    }

    [HttpPost]
    [Route("PoveziManekena/{manekenmbr}/{idmodnerevije}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LinkManekena(string manekenmbr, int idmodnerevije)
    {
        (bool isError1, var maneken, var error1) = await DataProvider.VratiManekena(manekenmbr);
        (bool isError2, var revija, var error2) = await DataProvider.VratiModnuRevijuAsync(idmodnerevije);

        if (isError1 || isError2)
        {
            return StatusCode(error1?.StatusCode ?? 400, $"{error1?.Message}{Environment.NewLine}{error2?.Message}");
        }

        if (maneken == null || revija == null)
        {
            return BadRequest("Radnik ili prodavnica nisu validni.");
        }

        var data = await DataProvider.DodajNastup(new NastupaView
        {
           

            Id = new NastupaIdView
            {
                NaReviji = revija,
                ManekenNaReviji = maneken
            }
        });

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Dodat odnos. Maneken: {maneken.Ime} {maneken.Prezime}. Revija: {revija.Naziv}");
    }

    [HttpPost]
    [Route("PoveziManekenaiModnuAgenciju/{manekenmbr}/{pib}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LinkManekenAgencija(string manekenmbr, string pib)
    {
        (bool isError1, var maneken, var error1) = await DataProvider.VratiManekena(manekenmbr);
        (bool isError2, var agencija, var error2) = await DataProvider.VratiModnuAgenciju(pib);

        if (isError1 || isError2)
        {
            return StatusCode(error1?.StatusCode ?? 400, $"{error1?.Message}{Environment.NewLine}{error2?.Message}");
        }

        if (maneken == null || agencija == null)
        {
            return BadRequest("Maneken ili agencija nisu validni.");
        }

        var result = await DataProvider.PoveziManekenaSaAgencijom(maneken, agencija);

        if (result.IsError)
        {
            return StatusCode(result.Error.StatusCode, result.Error.Message);
        }

        return Ok("Maneken uspešno povezan sa agencijom.");
    }


    [HttpGet]
    [Route("PreuzmiCasopiseManekena/{mbr}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetCasopisiManekena(string mbr)
    {
        (bool isError, var casopisi, var error) = DataProvider.VratiCasopiseManekena(mbr);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(casopisi);
    }

    [HttpPost]
    [Route("DodajCasopiseManekenu/{mbr}/{nazivcasopisa}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddCasopisiManekena(string mbr,string nazivcasopisa)
    {
        var data=await DataProvider.DodajCasopisManekenu(mbr, nazivcasopisa);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Dodat casopis. Maneken: {mbr}. Casopis: {nazivcasopisa}");
    }

    [HttpPost("KreirajManekena/{pib}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> KreirajManekena([FromBody] ManekenView m, string pib)
    {
        var (isError, mbr, error) = await DataProvider.SacuvajManekena(m, pib);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        

        return StatusCode(201, $"Upisan maneken, sa ID: {mbr}");
    }

    [HttpDelete]
    [Route("ObrisiCasopisManekena/{mbr}/{nazivcasopisa}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObrisiCasopisManekena(string mbr, string nazivcasopisa)
    {
        var result = await DataProvider.ObrisiCasopisManekena(mbr, nazivcasopisa);

        if (result.IsError)
        {
            return StatusCode(result.Error.StatusCode, result.Error.Message);
        }

        return Ok($"Časopis '{nazivcasopisa}' manekena sa MBR '{mbr}' je uspešno obrisan.");
    }



}