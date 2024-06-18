
using Microsoft.AspNetCore.Mvc;
using FashionWeekLibrary;
using FashionWeekLibrary.DTOs;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("[controller]")]
public class OrganizatorController : ControllerBase
{

    [HttpGet]
    [Route("PreuzmiOrganizatore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetOrganizatori()
    {
        (bool isError, var organizatori, ErrorMessage? error) = DataProvider.VratiSveOrganizatore();

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(organizatori);
    }

    [HttpPost]
    [Route("DodajOrganizatora")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddOrganizator([FromBody] OrganizatorView m)
    {
        var data = await DataProvider.DodajOrganizatora(m);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return StatusCode(201, $"Uspešno dodat organizator. ID: {m.OrganizatorID}");
    }

    [HttpPut]
    [Route("IzmeniOrganizatora")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateOrganizatora([FromBody] OrganizatorView ma)
    {
        (bool isError, var org, ErrorMessage? error) = await DataProvider.AzurirajOrganizatora(ma);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (org == null)
        {
            return BadRequest("Organizator nije validan.");
        }

        return Ok($"Uspešno ažuriran organizator. ID: {org.OrganizatorID}");
    }

    [HttpDelete]
    [Route("ObrisiOrganizatora/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteOrganizatora(int id)
    {
        var data = await DataProvider.ObrisiOrganizatora(id);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return StatusCode(204, $"Uspešno obrisan organizator. ID: {id}");
    }


    [HttpGet("PreuzmiRevijeOrganizatora/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetRevijeOrganizatora(int id)
    {
        var (isError, revije, error) = await DataProvider.VratiRevijeOrganizatora(id);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(revije);
    }
}




