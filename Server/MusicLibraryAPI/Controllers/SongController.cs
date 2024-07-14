using Microsoft.AspNetCore.Mvc;
using MusicLibraryBLL.DTOs;
using MusicLibraryBLL.Interfaces;
using MusicLibraryBLL.Services;
using MusicLibraryDAL.Repository;

namespace MusicLibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddSongAsync(CreateSongDTO song)
        {
            try
            {
                await _songService.AddSongAsync(song);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFromAlbum")]
        public async Task<IActionResult> GetAllSongsFromAlbumAsync(int albumId)
        {
            try
            {
                var songs = await _songService.GetAllSongsFromAlbumAsync(albumId);
                return Ok(songs);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSongAsync(SongDTO song)
        {
            try
            {
                await _songService.UpdateSongAsync(song);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteSongByIdAsync(int songId)
        {
            try
            {
                await _songService.DeleteSongByIdAsync(songId);
                return Ok();
            }
            catch (ServiceException ex)
            { 
                return BadRequest(ex.Message); 
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
