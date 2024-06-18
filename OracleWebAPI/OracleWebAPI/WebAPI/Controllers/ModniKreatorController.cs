using Microsoft.AspNetCore.Mvc;
using FashionWeekLibrary;
using FashionWeekLibrary.DTOs;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("[controller]")]
public class ModniKreatorController : ControllerBase
{
    [HttpGet]
    [Route("PreuzmiModneKreatore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetKreatori()
    {
        (bool isError, var kreatori, ErrorMessage? error) = DataProvider.VratiSveKreatore();

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(kreatori);
    }

    [HttpPost]
    [Route("DodajModnogKreatora")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddKreatora([FromBody] ModniKreatorView m)
    {
        var data = await DataProvider.DodajKreatora(m);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return StatusCode(201, $"Uspešno dodat modni kreator. Ime: {m.Ime}");
    }

    [HttpPost("KreirajModnogKreatoraOrg/{idOrganizatora}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> KreirajModnogKreatoraOrg([FromBody] ModniKreatorView m, int idOrganizatora)
    {
        var (isError, mbr, error) = await DataProvider.SacuvajModnogKreatora(m, idOrganizatora);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }



        return StatusCode(201, $"Upisan modni kreator sa mbr: {mbr}");
    }

    [HttpPut]
    [Route("IzmeniModnogKreatora")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateKreatora([FromBody] ModniKreatorView mk)
    {
        (bool isError, var kreator, ErrorMessage? error) = await DataProvider.AzurirajKreatora(mk);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (kreator == null)
        {
            return BadRequest("Kreator nije validan.");
        }

        return Ok($"Uspešno ažuriran kreator. ime: {kreator.Ime}");
    }

    [HttpDelete]
    [Route("ObrisiModnogKreatora/{mbr}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteKreatora(string mbr)
    {
        var data = await DataProvider.ObrisiKreatora(mbr);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        //return StatusCode(204, $"Uspešno obrisan kreator. mbr: {mbr}");
        return Ok();
    }

    [HttpGet]
    [Route("PreuzmiModneKreatoresaRevije/{IDModneRevije}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetKreatoriModneRevije(int IDModneRevije)
    {
        (bool isError, var kreatori, var error) = DataProvider.VratiSveModneKreatoreRevije(IDModneRevije);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(kreatori);
    }

    [HttpGet("PreuzmiModneKreatoreIzModneKuce/{naziv}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetModneKreatoreModneKuce(string naziv)
    {
        var (isError, kreatori, error) = await DataProvider.VratiSveModneKreatoreModneKuce(naziv);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        return Ok(kreatori);
    }

    [HttpPost]
    [Route("PoveziModnogKreatoraSaRevijom/{kreatormbr}/{idmodnerevije}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LinkKreatora(string kreatormbr, int idmodnerevije)
    {
        (bool isError1, var kreator, var error1) = await DataProvider.VratiModnogKreatora(kreatormbr);
        (bool isError2, var revija, var error2) = await DataProvider.VratiModnuRevijuAsync(idmodnerevije);

        if (isError1 || isError2)
        {
            return StatusCode(error1?.StatusCode ?? 400, $"{error1?.Message}{Environment.NewLine}{error2?.Message}");
        }

        if (kreator == null || revija == null)
        {
            return BadRequest("Nisu validni.");
        }

        var data = await DataProvider.DodajPredstavljanje(new PredstavljaView
        {


            Id = new PredstavljaIdView
            {
                NaModnojReviji = revija,
                MKPredstavlja = kreator
            }
        });

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Dodat odnos. Modni kreator: {kreator.Ime} {kreator.Prezime}. Revija: {revija.Naziv}");
    }


    




    [HttpPost("KreirajModnogkreatoraOrganizatora/{idOrganizatora}/{nazivModneKuce}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> KreirajModnogKreatoraOrg([FromBody] ModniKreatorView m, int idOrganizatora,string nazivModneKuce)
    {
        var (isError, mbr, error) = await DataProvider.SacuvajModnogKreatoraOrganizatora(m, idOrganizatora,nazivModneKuce);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }



        return StatusCode(201, $"Upisan modni kreator sa mbr: .{mbr}");
    }


    [HttpPost("KreirajModnogkreatora/{nazivModneKuce}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> KreirajModnogKreatora([FromBody] ModniKreatorView m, string nazivModneKuce)
    {
        var (isError, mbr, error) = await DataProvider.SacuvajModnogKreatora(m, nazivModneKuce);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }



        return StatusCode(201, $"Upisan modni kreator sa mbr: .{mbr}");
    }


}