using Microsoft.AspNetCore.Mvc;
using FashionWeekLibrary;
using FashionWeekLibrary.DTOs;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("[controller]")]
public class SpecijalniGostController : ControllerBase
{
    [HttpPost]
    [Route("DodajSpecijalnogGosta/{idRevije}/{mbrKreatora}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddGosta(int idRevije, string mbrKreatora)
    {
        var data = await DataProvider.DodajGosta(idRevije, mbrKreatora);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Dodat gost. revija: {idRevije}. Gost: {mbrKreatora}");
    }

    [HttpDelete]
    [Route("ObrisiGostaRevije/{idRevije}/{mbrKreatora}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteGosta(int idRevije, string mbrKreatora)
    {
        var result = await DataProvider.ObrisiSpecijalnogGosta(idRevije, mbrKreatora);

        if (result.IsError)
        {
            return StatusCode(result.Error.StatusCode, result.Error.Message);
        }

        return Ok($"Gost '{mbrKreatora}' sa revije  '{idRevije}' je uspešno obrisan.");
    }


    [HttpPut("AzurirajSpecijalnogGosta/{idGosta}/{mbrModniKreator}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AzurirajSpecijalnogGosta(int idGosta, string mbrModniKreator)
    {
        try
        {

            (bool isError1, var gost, var error1) = await DataProvider.VratiSpecijalnogGosta(idGosta);
            (bool isError2, var kreator, var error2) = await DataProvider.VratiModnogKreatora(mbrModniKreator);

            if (isError1 || isError2)
            {
                return StatusCode(error1?.StatusCode ?? 400, $"{error1?.Message}{Environment.NewLine}{error2?.Message}");
            }

            if (gost == null || kreator == null)
            {
                return BadRequest("Gost ili Kreator nisu validni.");
            }


            gost.MBRModniKreator= kreator;

            await DataProvider.AzurirajSpecijalnogGosta(gost);

            return Ok($"Specijalni gost sa ID-em {idGosta} uspešno ažuriran sa novim MBR-om modnog kreatora {mbrModniKreator}.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Došlo je do greške prilikom ažuriranja specijalnog gosta: {ex.Message}");
        }
    }
}

