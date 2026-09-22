using Pr06_Api.Interface;
using Pr06_Api.Model;
using System.Net.Http.Json;

namespace Pr06_Api.Services
{
    public class NasaHttpService : INasaHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public NasaHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["Nasa:ApiKey"] ?? "DEMO_KEY";
        }

        public async Task<Apod> GetApodAsync()
        {
            var client = _httpClientFactory.CreateClient("NasaClient");

            var response = await client.GetAsync($"apod?api_key={_apiKey}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Kunne ikke hente dagens billede: {error}");
            }

            var apod = await response.Content.ReadFromJsonAsync<Apod>();
            return apod ?? throw new Exception("Intet billede fundet.");
        }

        public async Task<Apod> GetApodByDateAsync(DateTime date)
        {
            var client = _httpClientFactory.CreateClient("NasaClient");
            var dateStr = date.ToString("yyyy-MM-dd");

            var response = await client.GetAsync($"apod?api_key={_apiKey}&date={dateStr}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Fejl ved hentning af billede for {dateStr}: {error}");
            }

            var apod = await response.Content.ReadFromJsonAsync<Apod>();
            return apod ?? throw new Exception($"Intet billede fundet for {dateStr}.");
        }

        public async Task<List<Apod>> GetApodByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var client = _httpClientFactory.CreateClient("NasaClient");
            var startStr = startDate.ToString("yyyy-MM-dd");
            var endStr = endDate.ToString("yyyy-MM-dd");

            var response = await client.GetAsync(
                $"apod?api_key={_apiKey}&start_date={startStr}&end_date={endStr}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Fejl ved hentning af billeder ({startStr} til {endStr}): {error}");
            }

            var apods = await response.Content.ReadFromJsonAsync<List<Apod>>();
            return apods ?? new List<Apod>();
        }
    }
}