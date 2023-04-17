using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FirstLab.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Web_App.Models;
using FirstLab.Business.Models.Request;
using FirstLab.Data.Models;

namespace Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INoteService _noteService;

        public HomeController(ILogger<HomeController> logger, INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }


        public async Task<IActionResult> Index(string? additionalSearch)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            var claims = User.Claims;
            string[] leha_byv_tyt = new string[2];
            int i = 0;

            foreach (var claim in claims)
            {
                leha_byv_tyt[i] = claim.Value;
                i++;
            }

            List<Note> notesList = new List<Note>();

            if (additionalSearch != null)
            {
                notesList = await _noteService.GetListOfNotesByUserRequest(leha_byv_tyt[0], additionalSearch);
            }
            else
            {
                notesList = await _noteService.GetListOfNotesByUserIdAsync(leha_byv_tyt[0]);
            }

            return View(notesList);
        }

        [HttpGet("/Home/ViewNote/{noteId}")]
        public async Task<IActionResult> ViewNote(string noteId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            var note = await _noteService.GetNoteByIdAsync(noteId);

            return View(note);
        }

        [HttpPost("Home/EditNote/{noteId}")]
        public async Task<IActionResult> EditNote(string noteId, NoteEditRequest noteEditRequest)
        {
            await _noteService.EditNoteAsync(noteEditRequest, noteId);


            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet("Home/EditNote/{noteId}")]
        public async Task<IActionResult> EditNote(string noteId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            var note = await _noteService.GetNoteByIdAsync(noteId);

            return View(note);
        }

        [HttpGet("Home/DeleteNote/{noteId}")]
        public async Task<IActionResult> DeleteNote(string noteId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            await _noteService.DeleteNoteByIdAsync(noteId);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(NoteRequest noteRequest)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            var claims = User.Claims;
            string[] leha_byv_tyt = new string[2];
            int i = 0;
            foreach (var claim in claims)
            {
                leha_byv_tyt[i] = claim.Value;
                i++;
            }

            await _noteService.AddNoteAsync(noteRequest, leha_byv_tyt[0]);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddNote()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNotesByFilter(AdditionalSearch additionalSearch)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            var claims = User.Claims;
            string[] leha_byv_tyt = new string[2];
            int i = 0;
            foreach (var claim in claims)
            {
                leha_byv_tyt[i] = claim.Value;
                i++;
            }

           return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}