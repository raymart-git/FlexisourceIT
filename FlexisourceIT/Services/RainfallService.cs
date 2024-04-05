using FlexisourceIT.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FlexisourceIT.Services
{
    public class RainfallService : IRainfallService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RainfallService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _baseUrl = apiSettings.Value.BaseUrl;
        }

        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<RainfallReading>> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            using (var httpClient = new HttpClient())
            {
                // Construct the URL for the API endpoint
                string apiUrl = $"{_baseUrl}{stationId}/readings?_sorted";

                try
                {
                    // Make the HTTP GET request
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response content into an array of RainfallReading objects
                        string responseData = await response.Content.ReadAsStringAsync();
                        try
                        {
                            RootObject rainfallReadings = JsonConvert.DeserializeObject<RootObject>(responseData);

                            if (count > 0)
                                return await Task.FromResult(rainfallReadings?.Items?.Take(count)?.ToList() ?? new List<RainfallReading>());
                                                            
                            return await Task.FromResult(rainfallReadings?.Items?.ToList() ?? new List<RainfallReading>());
                        }
                        catch (Exception ex)
                        {
                            return new List<RainfallReading>();
                        }
                    }
                    else
                    {
                        // If the request was not successful, throw an exception or handle the error as needed
                        throw new HttpRequestException($"Failed to get rainfall readings. Status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Log or handle the exception
                    Console.WriteLine($"Error occurred while fetching rainfall readings: {ex.Message}");
                    throw; // Rethrow the exception for the caller to handle
                }
            }
        }
    }
}
