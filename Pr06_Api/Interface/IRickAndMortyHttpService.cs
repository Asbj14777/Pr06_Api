using Pr06_Api.Model;

namespace Pr06_Api.Interface
{
    public interface IRickAndMortyHttpService
    {
        Task<Character?> GetCharacterByIdAsync(int id);
        Task<List<Character>> GetAllCharactersAsync();
    }
}
