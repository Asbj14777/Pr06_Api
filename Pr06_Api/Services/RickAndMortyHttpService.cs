using Pr06_Api.Interface;
using Pr06_Api.Model;
using System.Net.Http.Json;
using System.Text.Json;
namespace Pr06_Api.Services; 
public class RickAndMortyHttpService : IRickAndMortyHttpService
{
    private readonly HttpClient _httpClient;

    public RickAndMortyHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Character?> GetCharacterByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"character/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Character>();
    }
    public async Task<List<Character>> GetAllCharactersAsync()
    {
        var response = await _httpClient.GetAsync("character");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var document = JsonDocument.Parse(json);

        var results = document.RootElement.GetProperty("results");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return results.Deserialize<List<Character>>(options) ?? new List<Character>();
    }
}