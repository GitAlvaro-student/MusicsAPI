using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using MonitoringLogs.Models;
using MusicSoundAPI.Data.Artista;
using MusicSoundAPI.Models;
using MusicSoundAPI.Services.Artist;
using Serilog;
using System.Reflection;

namespace MusicSoundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        //private readonly ILogger<ArtistController> _logger;

        public ArtistsController(IArtistService artistService/*, ILogger<ArtistaController> logger*/)
        {
            _artistService = artistService;
            //_logger = logger;
        }

        /// <summary>
        /// Mostra informações de um Artista de acordo com o Id
        /// </summary>
        /// <param name="idArtist">Id do Artista a ser Mostrado</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a requisição seja feita com sucesso</response>
        [HttpGet("{idArtist}")]
        public async Task<IActionResult> GetArtistById(int idArtist)
        {
            var artist = await _artistService.GetArtistaById(idArtist);

            if (artist == null)
            {
                string app = Assembly.GetEntryAssembly().GetName().Name!;
                string source = HttpContext.Request.RouteValues.Values.First()!.ToString()!;
                var dict = new Dictionary<string, object>() { { "Properties", idArtist} };

                var nullLog = new FailureLog().SetLog(HttpContext, app,
                         source, LogLevel.Error.ToString()
                         , $"Artista nao encontrado", dict);
                
                Log.Error(nullLog);

                return NotFound("Artista Não Encontrado!!");
            }

            //var log = SetLog(LogLevel.Information.ToString(), "Sucesso!", "GetArtistById", idArtist);
            //Log.Information(log);

            return Ok(artist);
        }

        /// <summary>
        /// Mostra informações de uma lista de Artistas
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a requisição seja feita com sucesso</response>
        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artist = await _artistService.GetAllArtistas();
            var artistCount = artist.Count();

            if (artist == null)
            {
                //var nullLog = SetLog(LogLevel.Information.ToString(), $"Artistas não encontrados", "GetAllArtists");
                //Log.Information(nullLog);

                return NotFound("Artistas Não Encontrados!!");
            }

            //var log = SetLog(LogLevel.Information.ToString(), "Sucesso!", "GetAllArtists");
            //Log.Information(log);

            return Ok(new
            {
                Mensagem = $"{artistCount} Artistas Registrados",
                Dados = artist.OrderBy(m => m.Nome).Take(10)
            });
        }

        /// <summary>
        /// Insere informações de um Artista no Banco de Dados
        /// </summary>
        /// <param name="artist">Objeto necessário para inserir uma Artista</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [Route("RegisterArtist")]
        [HttpPost]
        public async Task<ActionResult<Artista>> PostArtist(CreateArtistDTO artist)
        {
            //_logger.LogInformation($"Executando Método {HttpContext.Request.PathBase}");
            try
            {
                if (!ModelState.IsValid)
                {
                    /*_logger.LogError($"Dados Inválidos Enviados" +
                        $"{artist}");*/
                    return BadRequest(ModelState);
                }

                await _artistService.InsertArtista(artist);
                return CreatedAtAction(nameof(GetAllArtists), new { name = artist.Nome }, artist);

            }
            catch (Exception ex)
            {
                //_logger.LogError($"O seguinte erro ocorreu {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

    }
}
