using api_for_kursach.DTO;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_for_kursach.Controllers
{
    public class RadioController : Controller
    {
        private readonly IRadioService _radioService;
        public RadioController(IRadioService service)
        {
            _radioService = service;
        }

        // GET: RadioController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRotation([FromBody] RotationApplicationDTO application)
        {
            return Ok(await _radioService.AddRotationAppilcation(application));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRotations()
        {
            return Ok(await _radioService.GetRotationApplications());
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDTO dto)
        {
            await _radioService.UpdateRotationStatus(dto.ApplicationId, dto.NewStatus);
            return Ok(new { message = "Статус успешно обновлён." });
        }

        // GET: RadioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public async Task< IActionResult> GetAllRadioStations()
        {
            return  Ok(await _radioService.GetRadioStations());

        }

        // GET: RadioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RadioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RadioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RadioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RadioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RadioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
