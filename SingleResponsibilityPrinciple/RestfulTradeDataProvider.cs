using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        private readonly string _url;
        private readonly ILogger _logger;

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            _url = url;
            _logger = logger;
        }

        public IEnumerable<string> GetTradeData()
        {
            return GetTradeDataAsync().GetAwaiter().GetResult();
        }

        private async Task<IEnumerable<string>> GetTradeDataAsync()
        {
            List<string> tradeData = new List<string>();
            _logger.LogInfo("Connecting to RESTful API at URL: " + _url);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(_url);
                    response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogInfo("Data received from RESTful API.");

                    // Deserialize JSON response to a list of trade strings
                    tradeData = JsonSerializer.Deserialize<List<string>>(jsonResponse);
                }
                catch (Exception ex)
                {
                    _logger.LogInfo("Error fetching trade data: " + ex.Message);
                    throw;
                }
            }

            return tradeData;
        }
    }
}
