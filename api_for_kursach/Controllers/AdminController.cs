using api_for_kursach.Repositories;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api_for_kursach.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtistsIncome()
        {
            var result = await _adminService.GetAllArtistsWithIncomeAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRotationApplications()
        {
            var result = await _adminService.GetAllRotationApplicationsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRotation(int id)
        {
            var success = await _adminService.ApproveRotationApplicationAsync(id);
            return success ? Ok() : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RejectRotation(int id)
        {
            var success = await _adminService.RejectRotationApplicationAsync(id);
            return success ? Ok() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArtist(int artistId)
        {
            var success = await _adminService.DeleteArtistAsync(artistId);
            return success ? Ok() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAlbum(int albumId)
        {
            var success = await _adminService.DeleteAlbumWithTracksAndRotationsAsync(albumId);
            return success ? Ok() : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseRoyalty(int artistId, decimal amount)
        {
            var success = await _adminService.DecreaseRoyaltyAsync(artistId, amount);
            return success ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetRotationStats()
        {
            var result = await _adminService.GetRotationStatisticsAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtistIncomeStats()
        {
            var result = await _adminService.GetArtistIncomeStatisticsAsync();
            return Ok(result);
        }
    }
}
