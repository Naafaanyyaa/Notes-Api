using FirstLab.Data.Interfaces;
using FirstLab.Data.Models;
using MongoDB.Driver;

namespace FirstLab.Data.Repository
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        private readonly IMongoCollection<Note> _entity;

        public NoteRepository(IMongoDbFactory mongoDbFactory) : base(mongoDbFactory, "notes")
        {
            _entity = mongoDbFactory.GetCollection<Note>("lab_work", "notes");
        }

        public async Task<List<Note>> GetNotesByUserId(string userId)
        {
            using var result = await _entity.FindAsync(x => x.UserId == Guid.Parse(userId));

            return await result.ToListAsync();
        }

        public override async Task<Note> UpdateAsync(Note entity)
        {
            _entity.FindOneAndUpdateAsync(x => x.Id == entity.Id, Builders<Note>.Update.Set(x => x.Title, entity.Title)).ConfigureAwait(false);
            _entity.FindOneAndUpdateAsync(x => x.Id == entity.Id, Builders<Note>.Update.Set(x => x.LastModifiedDate, entity.LastModifiedDate)).ConfigureAwait(false);
            var result = await _entity.FindOneAndUpdateAsync(x => x.Id == entity.Id, Builders<Note>.Update.Set(x => x.Text, entity.Text)).ConfigureAwait(false);

            return result;
        }

        public async Task<List<Note>> GetByRequestAsync(string userId, string request)
        {
            using var result = await _entity.FindAsync(x => x.UserId == Guid.Parse(userId) && (x.Text.Contains(request.Trim()) || x.Title.Contains(request.Trim())));
            return await result.ToListAsync();
        }

    }
}
