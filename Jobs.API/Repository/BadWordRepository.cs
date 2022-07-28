using Jobs.API.Data;
using Jobs.API.Entities;
using Jobs.API.Interface;
using MongoDB.Driver;

namespace Jobs.API.Repository
{
    public class BadWordRepository : IBadWordRepository
    {
        private readonly IJobContext _context;

        public BadWordRepository(IJobContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateBadWord(BadWord badWord)
        {
            await _context.BadWords.InsertOneAsync(badWord);
        }

        public async Task<bool> DeleteBadWord(string id)
        {
            FilterDefinition<BadWord> filter = Builders<BadWord>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .BadWords
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<BadWord> GetBadWord(string id)
        {
            return await _context
                           .BadWords
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<BadWord> GetBadWordByWord(string word)
        {
            return await _context
                           .BadWords
                           .Find(p => p.Word == word)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BadWord>> GetBadWords()
        {
            return await _context
                            .BadWords
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> UpdateBadWord(BadWord badWord)
        {
            var updateResult = await _context
                                        .BadWords
                                        .ReplaceOneAsync(filter: g => g.Id == badWord.Id, replacement: badWord);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
