using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLab.Business.Models.Request;
using FirstLab.Data.Models;

namespace FirstLab.Business.Interfaces
{
    public interface INoteService
    {
        Task<Note> GetNoteByIdAsync(string id);
        Task<Note> AddNoteAsync(NoteRequest note, string userId);
        Task<List<Note>> GetListOfNotesByUserIdAsync(string userId);
        Task DeleteNoteByIdAsync(string noteId);
        Task<Note> EditNoteAsync(NoteEditRequest noteEditRequest, string noteId);
        Task<List<Note>> GetListOfNotesByUserRequest(string userId, string request);
        Task<List<Note>> GetListOfNotesByUserRequest(string userId, AdditionalSearch request);
    }
}
