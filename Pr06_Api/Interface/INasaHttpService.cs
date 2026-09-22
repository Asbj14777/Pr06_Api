using Pr06_Api.Model;

namespace Pr06_Api.Interface
{
    public interface INasaHttpService
    {
        Task<Apod> GetApodAsync();
        Task<Apod> GetApodByDateAsync(DateTime date);
        Task<List<Apod>> GetApodByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}