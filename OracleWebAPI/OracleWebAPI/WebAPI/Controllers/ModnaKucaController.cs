
using Microsoft.AspNetCore.Mvc;
using FashionWeekLibrary;
using FashionWeekLibrary.DTOs;
using Microsoft.AspNetCore.Cors;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("[controller]")]
public class ModnaKucaController : ControllerBase
{

    [HttpGet]
    [Route("PreuzmiModneKuce")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetModneKuce()
    {
        (bool isError, var kuce, ErrorMessage? error) = DataProvider.VratiSveModneKuce();

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(kuce);
    }

    [HttpPost]
    [Route("DodajModnuKucu")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddModnaKuca([FromBody] ModnaKucaView m)
    {
        var data = await DataProvider.DodajModnuKucu(m);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return StatusCode(201, $"Uspešno dodata modna kuca. Naziv: {m.Naziv}");
    }

    [HttpPut]
    [Route("IzmeniModnuKucu")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateModnaKuca([FromBody] ModnaKucaView ma)
    {
        (bool isError, var kuca, ErrorMessage? error) = await DataProvider.AzurirajModnuKucu(ma);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (kuca == null)
        {
            return BadRequest("Kuca nije validna.");
        }

        return Ok($"Uspešno ažurirana kuca. Naziv: {kuca.Naziv}");
    }

    [HttpDelete]
    [Route("ObrisiModnuKucu/{naziv}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteKucu(string naziv)
    {
        var data = await DataProvider.ObrisiModnuKucu(naziv);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok();
    }


    [HttpGet]
    [Route("PreuzmiImenaVlasnikaModneKuce/{naziv}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetVlasniciModneKuce(string naziv)
    {
        (bool isError, var vlasnici, var error) = DataProvider.VratiImenaVlasnikaModneKuce(naziv);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(vlasnici);
    }

    [HttpPost("KreirajModnuKucu")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> KreirajModnuKucu([FromBody] ModnaKucaView m, int idOrg)
    {
        
        var (isError, naziv, error) = await DataProvider.SacuvajModnuKucu(m,idOrg);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }



        return StatusCode(201, $"Upisana modna kuca sa Nazivom: .{naziv}");
    }


    [HttpPost("KreirajModnuKucuPayload")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> KreirajModnuKucu([FromBody] ModnaKucaRequestModel m)
    {
        int idOrg = m.OrganizatorID;
        var (isError, naziv, error) = await DataProvider.SacuvajModnuKucu(m.ModnaKuca, idOrg);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }



        return StatusCode(201, $"Upisana modna kuca sa Nazivom: .{naziv}");
    }

    public class ModnaKucaRequestModel
    {
        public ModnaKucaView? ModnaKuca { get; set; }
        public int OrganizatorID { get; set; }
    }

}




