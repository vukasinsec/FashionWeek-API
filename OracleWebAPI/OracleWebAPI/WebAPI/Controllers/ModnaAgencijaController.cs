
    using Microsoft.AspNetCore.Mvc;
    using FashionWeekLibrary;
    using FashionWeekLibrary.DTOs;
    using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers;

    [ApiController]
    [EnableCors("CORS")]
    [Route("[controller]")]
    public class ModnaAgencijaController : ControllerBase
    {

    [HttpGet]
    [Route("PreuzmiModneAgencije")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetModneAgencije()
    {
        (bool isError, var agencije, ErrorMessage? error) = DataProvider.VratiSveModneAgencije();

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(agencije);
    }

    [HttpPost]
    [Route("DodajModnuAgenciju")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddModnaAgencija([FromBody] ModnaAgencijaView m)
    {
        var data = await DataProvider.DodajModnuAgencijuAsync(m);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return StatusCode(201, $"Uspešno dodata modna agencija. Naziv: {m.Naziv}");
    }

    [HttpPut]
    [Route("IzmeniModnuAgenciju")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateModnuAgenciju([FromBody] ModnaAgencijaView ma)
    {
        (bool isError, var agencija, ErrorMessage? error) = await DataProvider.AzurirajModnuAgenciju(ma);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (agencija == null)
        {
            return BadRequest("Agencija nije validna.");
        }

        return Ok($"Uspešno ažurirana agencija. Naziv: {agencija.Naziv}");
    }

    [HttpDelete]
    [Route("ObrisiModnuAgenciju/{pib}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteAgenciju(string pib)
    {
        var data = await DataProvider.ObrisiModnuAgencijuAsync(pib);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        //return StatusCode(204, $"Uspešno obrisana agencija. PIB: {pib}");
        return Ok();
    }

    [HttpGet]
    [Route("PrikaziZemljeModneAgencije/{pib}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetZemlje(string pib)
    {
        (bool isError, var zemlje, var error) = await DataProvider.VratiNaziveZemalja(pib);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(zemlje);
    }


    [HttpPost]
    [Route("DodajZemljeAgenciji/{pib}/{nazivZemlje}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddZemlje(string pib, string nazivZemlje)
    {
        var data = await DataProvider.DodajZemljeAgencije(pib, nazivZemlje);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Dodata Zemlja. Agencija: {pib}. Zemlja: {nazivZemlje}");
    }

    [HttpDelete]
    [Route("ObrisiZemljuAgencije/{pib}/{nazivZemlje}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteZemlju(string pib, string nazivZemlje)
    {
        var result = await DataProvider.ObrisiZemljuAgencije(pib, nazivZemlje);

        if (result.IsError)
        {
            return StatusCode(result.Error.StatusCode, result.Error.Message);
        }

        return Ok($"Zemlja '{nazivZemlje}' agencija sa PIB '{pib}' je uspešno obrisan.");
    }


}




