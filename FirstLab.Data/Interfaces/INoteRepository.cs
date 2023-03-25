using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLab.Data.Models;
using SavePets.Data.Interfaces;

namespace FirstLab.Data.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<List<Note>> GetNotesByUserId(string userId);
        Task<List<Note>> GetByRequestAsync(string userId, string request);
    }
}
