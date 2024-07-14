using Microsoft.AspNetCore.Mvc;
using MusicLibraryBLL.DTOs;
using MusicLibraryBLL.Interfaces;
using MusicLibraryBLL.Services;
using MusicLibraryDAL.Repository;

namespace MusicLibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAlbumAsync(CreateAlbumDTO album)
        {
            try
            {
                await _albumService.AddAlbumAsync(album);
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

        [HttpGet("GetAlbumsFromArtist")]
        public async Task<IActionResult> GetAllAlbumsFromArtistAsync(int artistId)
        {
            try
            {
                var albums = await _albumService.GetAllAlbumsFromArtistAsync(artistId);
                return Ok(albums);
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
        public async Task<IActionResult> UpdateAlbumAsync(AlbumDTO album)
        {
            try
            {
                await _albumService.UpdateAlbumAsync(album);
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
        public async Task<IActionResult> DeleteAlbumAsync(int albumId)
        {
            try
            {
                await _albumService.DeleteAlbumByIdAsync(albumId);
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
