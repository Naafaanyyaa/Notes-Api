using FirstLab.Business.Interfaces;
using FirstLab.Business.Models.Request;
using FirstLab.Business.Services;
using FirstLab.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirstLab.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("get-note-by-id/{noteId}")]
        [Authorize]
        [ProducesResponseType(typeof(Note), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNoteByIdAsync(string noteId)
        {
            var result = await _noteService.GetNoteByIdAsync(noteId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("get-notes-by-userid")]
        [Authorize]
        [ProducesResponseType(typeof(List<Note>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListOfNotesByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _noteService.GetListOfNotesByUserIdAsync(userId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("get-notes-by-request")]
        [Authorize]
        [ProducesResponseType(typeof(List<Note>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListOfNotesByUserRequest(string request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _noteService.GetListOfNotesByUserRequest(userId, request);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("get-notes-by-additional-request")]
        [Authorize]
        [ProducesResponseType(typeof(List<Note>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListOfNotesByUserAdditionalRequest([FromQuery] AdditionalSearch request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _noteService.GetListOfNotesByUserRequest(userId, request);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("add-note")]
        [Authorize]
        [ProducesResponseType(typeof(NoteRequest), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddNoteAsync(NoteRequest noteRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _noteService.AddNoteAsync(noteRequest, userId);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete("delete/{noteId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(string noteId)
        {
            await _noteService.DeleteNoteByIdAsync(noteId);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("update/{noteId}")]
        [Authorize]
        [ProducesResponseType(typeof(Note), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditNoteAsync(NoteEditRequest noteEditRequest, string noteId)
        {
            var result = await _noteService.EditNoteAsync(noteEditRequest, noteId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

    }
}
