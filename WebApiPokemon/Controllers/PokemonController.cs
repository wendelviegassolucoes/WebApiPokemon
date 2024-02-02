using Microsoft.AspNetCore.Mvc;
using WebApiPokemon.Dto;
using WebApiPokemon.Services;

namespace WebApiPokemon.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PokemonController : ControllerBase
{
    [HttpGet(Name = "GetPokemons")]
    public ActionResult<PokemonDto> GetPokemons(int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            PokemonAppService pokemonAppService = new();
            return Ok(pokemonAppService.GetPokemonsDtoPagination(pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            // Trate qualquer exceção e retorne um erro HTTP 500
            return StatusCode(500, $"Erro ao recuperar Pokémons: {ex.Message}");
        }
    }

    [HttpGet(Name = "GetAllPokemons")]
    public async Task<ActionResult<PokemonDto>> GetAllPokemons()
    {
        try
        {
            PokemonAppService pokemonAppService = new();
            return Ok(await pokemonAppService.GetAllPokemonsDtoPaginationAsync());
        }
        catch (Exception ex)
        {
            // Trate qualquer exceção e retorne um erro HTTP 500
            return StatusCode(500, $"Erro ao recuperar Pokémons: {ex.Message}");
        }
    }

    [HttpPut(Name = "FeedPokemon")]
    public ActionResult FeedPokemon(string pokemonId)
    {
        try
        {
            bool feeded = new PokemonAppService().FeedPokemon(pokemonId);

            if (feeded)
            {
                return Ok();
            }

            return BadRequest();

        }
        catch (Exception ex)
        {
            // Trate qualquer exceção e retorne um erro HTTP 500
            return StatusCode(500, $"Erro ao recuperar Pokémons: {ex.Message}");
        }
    }

    [HttpPut(Name = "KillPokemon")]
    public ActionResult KillPokemon(string pokemonId)
    {
        try
        {
            bool killed = new PokemonAppService().KillPokemon(pokemonId);

            if (killed)
            {
                return Ok();
            }

            return BadRequest();

        }
        catch (Exception ex)
        {
            // Trate qualquer exceção e retorne um erro HTTP 500
            return StatusCode(500, $"Erro ao recuperar Pokémons: {ex.Message}");
        }
    }

    [HttpPut(Name = "RevivePokemons")]
    public ActionResult RevivePokemons()
    {
        try
        {
            bool killed = new PokemonAppService().RevivePokemons();

            if (killed)
            {
                return Ok();
            }

            return BadRequest();

        }
        catch (Exception ex)
        {
            // Trate qualquer exceção e retorne um erro HTTP 500
            return StatusCode(500, $"Erro ao recuperar Pokémons: {ex.Message}");
        }
    }

    //[HttpPost(Name = "InsertPokemon")]
    //public async Task<ActionResult<bool>> InsertPokemon()
    //{
    //    try
    //    {
    //        PokemonAppService pokemonAppService = new();
    //        return Ok(await pokemonAppService.InsertPokemonAsync());
    //    }
    //    catch (Exception ex)
    //    {
    //        // Trate qualquer exceção e retorne um erro HTTP 500
    //        return StatusCode(500, $"Erro ao recuperar Pokémons: {ex.Message}");
    //    }
    //}

    //[HttpPost(Name = "InsertForms")]
    //public async Task<ActionResult<bool>> InsertForms()
    //{
    //    try
    //    {
    //        PokemonAppService pokemonAppService = new();
    //        return Ok(pokemonAppService.InsertFormsAsync());
    //    }
    //    catch (Exception ex)
    //    {
    //        // Trate qualquer exceção e retorne um erro HTTP 500
    //        return StatusCode(500, $"Erro ao recuperar Pokémons: {ex.Message}");
    //    }
    //}
}