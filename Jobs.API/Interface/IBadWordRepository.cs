using Jobs.API.Entities;

namespace Jobs.API.Interface
{
    public interface IBadWordRepository
    {
        Task<IEnumerable<BadWord>> GetBadWords();
        Task<BadWord> GetBadWord(string id);
        Task<BadWord> GetBadWordByWord(string word);
        Task CreateBadWord(BadWord badWord);
        Task<bool> UpdateBadWord(BadWord badWord);
        Task<bool> DeleteBadWord(string id);
    }
}
