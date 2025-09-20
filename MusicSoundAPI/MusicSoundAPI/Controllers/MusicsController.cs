using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicSoundAPI.Data.Musica;
using MusicSoundAPI.Services.Music;
using Serilog;

namespace MusicSoundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly IMusicService _musicService;


        public MusicsController(IMusicService musicService)
        {
            _musicService = musicService;
        }

        /// <summary>
        /// Mostra informações de uma Música de acordo com o Id
        /// </summary>
        /// <param name="idMusic">Id da Música a ser Mostrada</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a requisição seja feita com sucesso</response>
        [HttpGet("byMusicId")]
        public async Task<IActionResult> GetMusicById(int idMusic)
        {
            var song = await _musicService.GetMusicById(idMusic);

            /*var log = SetLog(LogLevel.Information.ToString(), "Sucesso!", "GetMusicById", idMusic);
            Log.Information(log);*/

            return Ok(song);
        }

        /// <summary>
        /// Mostra informações de uma lista de Músicas
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a requisição seja feita com sucesso</response>
        [HttpGet("AllMusics")]
        public async Task<IActionResult> GetAllMusics()
        {
            var song = await _musicService.GetMusics();

            /*var log = SetLog(LogLevel.Information.ToString(), "Sucesso!", "GetAllMusics");
            Log.Information(log);*/

            return Ok(song);
        }

        /// <summary>
        /// Insere informações de uma Música no Banco de Dados
        /// </summary>
        /// <param name="music">Objeto necessário para inserir uma Música</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost("RegisterMusic")]
        public async Task<IActionResult> PostMusic(CreateMusicDTO music)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _musicService.InsertMusic(music);

            /*var log = SetLog(LogLevel.Information.ToString(), "Sucesso!", "PostMusic", music);
            Log.Information(log);*/

            return CreatedAtAction(nameof(PostMusic), music);

        }
    }
}
