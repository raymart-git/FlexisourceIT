using FlexisourceIT.Models;
using Refit;

namespace FlexisourceIT.Services
{
    public interface IRainfallService
    {
        Task<List<RainfallReading>> GetRainfallReadingsAsync(string stationId, int count = 10);
    }
}
